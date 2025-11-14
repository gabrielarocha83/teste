using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;
using Yara.WebApi.ViewModel;

namespace Yara.WebApi.Controllers
{
    [RoutePrefix("bucket")]
    [Authorize]
    public class AmazonS3Controller : ApiController
    {
        private readonly IAppServiceAmazonS3 _amazonS3;

        private readonly string _access = ConfigurationManager.AppSettings["AmazonAcessKey"];
        private readonly string _secret = ConfigurationManager.AppSettings["AmazonSecretKey"];
        private readonly string _region = ConfigurationManager.AppSettings["AmazonRegion"];
        private readonly string _bucketName = ConfigurationManager.AppSettings["BucketName"];

        public AmazonS3Controller(IAppServiceAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3;
        }

        [HttpPost]
        [Route("v1/getlogsamazon")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "logArquivamento_Acesso")]
        public async Task<GenericResult<IQueryable<AmazonS3InfoDto>>> GetLogsAmazon(ODataQueryOptions<AmazonS3InfoDto> options, AmazonS3SearchDto periodo)
        {
            var result = new GenericResult<IQueryable<AmazonS3InfoDto>>();

            try
            {
                var arquivos = await _amazonS3.ListingObjectsAsync(_access, _secret, _region, _bucketName, periodo);
                if (arquivos != null)
                {
                    int totalReg = 0;
                    if (options.Filter != null)
                    {
                        var filtro = options.Filter.ApplyTo(arquivos.AsQueryable(), new ODataQuerySettings()).Cast<AmazonS3InfoDto>();
                        totalReg = filtro.Count();
                    }
                    result.Result = options.ApplyTo(arquivos.AsQueryable()).Cast<AmazonS3InfoDto>();
                    result.Count = arquivos.Count();
                }

                result.Success = true;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Errors = new[] { e.Message };
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;
        }

        [HttpGet]
        [Route("v1/downloadlogsamazon")]
        [ClaimsAutorize(ClaimType = "Permissao", ClaimValue = "logArquivamento_Acesso")]
        public async Task<HttpResponseMessage> DownloadLogsAmazon(string objectKey)
        {
            var result = new HttpResponseMessage();

            try
            {
                var arquivo = await _amazonS3.DownloadObjectAsync(_access, _secret, _region, _bucketName, objectKey);

                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(arquivo);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = objectKey
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            catch (Exception e)
            {
                var logger = log4net.LogManager.GetLogger("YaraLog");
                logger.Error(e.Message);
            }

            return result;
        }
    }
}
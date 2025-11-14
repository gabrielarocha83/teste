using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.AppService.Interfaces;

#pragma warning disable CS1998 // O método assíncrono não possui operadores 'await' e será executado de forma síncrona

namespace Yara.AppService
{
    public class AppServiceAmazonS3 : IAppServiceAmazonS3
    {

        public async Task<IEnumerable<AmazonS3InfoDto>> ListingObjectsAsync(string access, string secret, string region, string bucketName, AmazonS3SearchDto periodo)
        {
            List<AmazonS3InfoDto> result = null;

            using (IAmazonS3 client = new AmazonS3Client(new BasicAWSCredentials(access, secret), RegionEndpoint.GetBySystemName(region)))
            {
                result = new List<AmazonS3InfoDto>();

                ListObjectsV2Request request = new ListObjectsV2Request { BucketName = bucketName };
                ListObjectsV2Response response;

                do
                {
                    response = await client.ListObjectsV2Async(request);
                    if (response != null)
                    {
                        var items = response.S3Objects.Where(c => c.LastModified >= periodo.DataInicio && c.LastModified <= periodo.DataFim).ToList();
                        foreach (var item in items)
                        {
                            AmazonS3InfoDto file = new AmazonS3InfoDto()
                            {
                                Name = item.Key,
                                Date = item.LastModified,
                                Size = item.Size
                            };
                            result.Add(file);
                        }
                    }
                    request.ContinuationToken = response.ContinuationToken;
                } while (response.IsTruncated);
            }

            return result;
        }

        public async Task<byte[]> DownloadObjectAsync(string access, string secret, string region, string bucketName, string objectKey)
        {
            byte[] result = null;

            using (IAmazonS3 client = new AmazonS3Client(new BasicAWSCredentials(access, secret), RegionEndpoint.GetBySystemName(region)))
            {
                var response = await client.GetObjectAsync(bucketName, objectKey);
                using (MemoryStream ms = new MemoryStream())
                {
                    response.ResponseStream.CopyTo(ms);
                    result = ms.ToArray();
                }
            }

            return result;
        }

    }
}

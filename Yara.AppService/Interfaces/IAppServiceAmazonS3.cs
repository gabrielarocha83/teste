using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceAmazonS3
    {
        Task<IEnumerable<AmazonS3InfoDto>> ListingObjectsAsync(string access, string secret, string region, string bucketName, AmazonS3SearchDto periodo);

        Task<byte[]> DownloadObjectAsync(string access, string secret, string region, string bucketName, string objectKey);
    }
}

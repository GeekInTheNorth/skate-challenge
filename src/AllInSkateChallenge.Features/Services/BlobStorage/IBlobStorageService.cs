namespace AllInSkateChallenge.Features.Services.BlobStorage
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IBlobStorageService
    {
        Task<string> StoreFile(string fileName, Stream fileData, string contentType);

        Task DeleteFile(string fileUrl);
    }
}
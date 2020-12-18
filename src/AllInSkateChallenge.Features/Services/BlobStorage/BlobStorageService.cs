namespace AllInSkateChallenge.Features.Services.BlobStorage
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Web;

    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;

    using Microsoft.Extensions.Options;

    public class BlobStorageService : IBlobStorageService
    {
        private readonly StorageSettings settings;

        public BlobStorageService(IOptions<StorageSettings> storageSettings)
        {
            settings = storageSettings.Value;
        }

        public async Task<string> StoreFile(string fileName, Stream fileData, string contentType)
        {
            var blobContainer = new BlobContainerClient(settings.ConnectionString, settings.ProfileContainer);
            await blobContainer.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blobClient = blobContainer.GetBlobClient(fileName);
            var httpHeaders = new BlobHttpHeaders { ContentType = contentType };
            await blobClient.UploadAsync(fileData, httpHeaders);

            var uriBuilder = new UriBuilder(blobClient.Uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["v"] = DateTime.Now.Ticks.ToString();
            uriBuilder.Query = query.ToString();

            return uriBuilder.ToString();
        }

        public async Task DeleteFile(string fileUrl)
        {
            var uri = new Uri(fileUrl);
            var filename = Path.GetFileName(uri.LocalPath);

            var blobContainer = new BlobContainerClient(settings.ConnectionString, settings.ProfileContainer);
            await blobContainer.DeleteBlobIfExistsAsync(filename, DeleteSnapshotsOption.IncludeSnapshots);
        }
    }
}
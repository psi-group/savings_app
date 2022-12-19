using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class FileSaver : IFileSaver
    {
        private readonly IConfiguration _config;
        public FileSaver(IConfiguration config)
        {
            _config = config;
        }
        public async Task SaveImage(IFormFile imageFile, string fileName, bool isProduct)
        {
            var connectionString = _config["ImageStorage:azureBlobStorageConnString"];
            var containerName = isProduct ?
                _config["ImageStorage:AzureBlobContainerName:ProductImg"] :
                _config["ImageStorage:AzureBlobContainerName:UserImg"];
            var fileExtention = _config["ImageStorage:ImageExtention"];
            //BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            BlockBlobClient blobClient = new BlockBlobClient(connectionString, containerName, fileName + fileExtention);

            using (var filestream = new MemoryStream())
            {
                await imageFile.CopyToAsync(filestream);
                filestream.Position = 0;
                //await containerClient.UploadBlobAsync(fileName + fileExtention, filestream);

                await blobClient.UploadAsync(filestream);
            }
        }
    }
}

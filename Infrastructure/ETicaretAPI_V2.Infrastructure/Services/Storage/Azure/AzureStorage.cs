using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ETicaretAPI_V2.Application.Abstraction.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ETicaretAPI_V2.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage : Storage, IAzureStorage
    {
        readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);
        }
        public async Task DeleteAsync(string container, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            BlobClient blobClient= _blobContainerClient.GetBlobClient(fileName);
             await blobClient.DeleteAsync();

        }

        public List<string> GetFiles(string container)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            return _blobContainerClient.GetBlobs().Select(b=>b.Name).ToList();
        }

        public bool HasFile(string container, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            return _blobContainerClient.GetBlobs().Any(b=>b.Name == fileName);
        }

        public async Task<List<(string fileName, string pathOrContainer)>> UploadAsync(IFormFileCollection files, string container)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(container);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
            List<(string fileName, string pathOrContainer)> datas = new();
            foreach (IFormFile file in files)
            {
                string fileNewName= await FileRenameAsync(container, HasFile, file.Name);
                BlobClient blobClient= _blobContainerClient.GetBlobClient(fileNewName);
                await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((fileNewName, container));
            }
            return datas;
        }
    }
}

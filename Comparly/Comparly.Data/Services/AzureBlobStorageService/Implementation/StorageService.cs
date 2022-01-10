using Azure.Storage.Blobs;
using Comparly.Data.Services.AzureBlobStorageService.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparly.Data.Services.AzureBlobStorageService.Implementation
{
    public class StorageService : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public StorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        
        public void Upload(IFormFile formFile)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("");
            var blobClient = containerClient.GetBlobClient(formFile.FileName);
            using (var stream = formFile.OpenReadStream())
            {
                blobClient.UploadAsync(stream, true);
            }
        }
    }
}

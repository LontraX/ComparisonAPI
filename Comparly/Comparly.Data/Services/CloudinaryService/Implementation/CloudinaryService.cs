using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Comparly.Data.Dtos;
using Comparly.Data.Services.CloudinaryService.Interface;
using Comparly.Data.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparly.Data.Services.CloudinaryService.Implementation
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly CloudinaryConfig _config;
        public Cloudinary _cloudinary { get; set; }
        public CloudinaryService(IOptions<CloudinaryConfig> config)
        {
            _config = config.Value;
            Account account = new Account
            (
                _config.CloudName,
                _config.ApiKey,
                _config.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }
        public DeletionResult DeleteFile(string publicId)
        {
            var delParams = new DeletionParams(publicId) { ResourceType = ResourceType.Raw };
            return _cloudinary.Destroy(delParams);
        }

        public async Task<FileUploadResponseDto> UploadFile(IFormFile file)
        {
            var fileUpload = new RawUploadResult();
            
            using (var fs = file.OpenReadStream())
            {
                var fileUploadParams = new BasicRawUploadParams()
                {
                    File = new FileDescription(file.FileName, fs),
                };
                fileUpload = await _cloudinary.UploadLargeRawAsync(fileUploadParams);
            }
            var fileUrl = fileUpload.Url.ToString();
            
            FileUploadResponseDto response = new FileUploadResponseDto()
            {
                FileUrl = fileUrl,
                PublicId = fileUpload.PublicId
            };
            return response;
        }
    }
}

using Comparly.Data.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparly.Data.Services.CloudinaryService.Interface
{
    public interface ICloudinaryService
    {
        public Task<FileUploadResponseDto> UploadFile(IFormFile file);
        CloudinaryDotNet.Actions.DeletionResult DeleteFile(string publicId);
    }
}

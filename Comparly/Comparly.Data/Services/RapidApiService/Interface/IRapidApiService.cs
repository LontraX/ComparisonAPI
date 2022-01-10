using Comparly.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparly.Data.Services.RapidApiService.Interface
{
    public interface IRapidApiService
    {
        Task<RapidApiResponseDto> GetTextSimilarity(string text1, string text2);
    }
}

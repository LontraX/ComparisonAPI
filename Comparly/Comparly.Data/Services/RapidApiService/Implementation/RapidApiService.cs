using Comparly.Data.Dtos;
using Comparly.Data.Services.RapidApiService.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Comparly.Data.Services.RapidApiService.Implementation
{
    public class RapidApiService : IRapidApiService
    {
        private readonly string _apiKey;
        private readonly string _apiHost;

        public RapidApiService(IConfiguration configuration)
        {
            _apiKey = configuration.GetSection("RapidApiConfig:ApiKey").Value;
            _apiHost = configuration.GetSection("RapidApiConfig:ApiHost").Value;
        }

        
        public async Task<RapidApiResponseDto> GetTextSimilarity(string text1, string text2)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://twinword-text-similarity-v1.p.rapidapi.com/similarity/"),
                Headers =
                {
                    { "x-rapidapi-host", _apiHost },
                    { "x-rapidapi-key", _apiKey },
                },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "text1", text1},
                    { "text2", text2 },
                }),
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var stuff = JsonConvert.DeserializeObject<RapidApiResponseDto>(body);
                return stuff;
            }
        }
    }
}

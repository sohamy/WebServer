using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebpServer.External
{
    public class TimeApiClient
    {
        private readonly HttpClient _httpClient;

        public TimeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // SSL 문제 회피용: 학습/테스트 환경이니까 HTTP 사용
            _httpClient.BaseAddress ??= new Uri("http://worldtimeapi.org/api/");
        }

        public async Task<string> GetSeoulTimeRawAsync()
        {
            var response = await _httpClient.GetAsync("timezone/Asia/Seoul");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}

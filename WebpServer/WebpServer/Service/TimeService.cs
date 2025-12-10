// Service/TimeService.cs
using System;
using System.Text.Json;
using System.Threading.Tasks;
using WebpServer.External;

namespace WebpServer.Services
{
    public class TimeService : ITimeService
    {
        private readonly TimeApiClient _timeApiClient;

        public TimeService(TimeApiClient timeApiClient)
        {
            _timeApiClient = timeApiClient;
        }

        public async Task<DateTimeOffset> GetSeoulTimeAsync()
        {
            var json = await _timeApiClient.GetSeoulTimeRawAsync();

            // worldtimeapi.org 응답에서 datetime 필드만 뽑는 예시
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            var dtString = root.GetProperty("datetime").GetString();

            return DateTimeOffset.Parse(dtString!);
        }
    }
}

// Controllers/TimeController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebpServer.External;

namespace WebpServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly TimeApiClient _timeApiClient;

        public TimeController(TimeApiClient timeApiClient)
        {
            _timeApiClient = timeApiClient;
        }

        // GET /time/seoul
        [HttpGet("seoul")]
        public async Task<IActionResult> GetSeoulTime()
        {
            var json = await _timeApiClient.GetSeoulTimeRawAsync();

            // 일단은 JSON 문자열 그대로 내려주기
            return Content(json, "application/json");
        }
    }
}

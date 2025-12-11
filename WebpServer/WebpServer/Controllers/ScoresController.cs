using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebpServer.Protocol;
using WebpServer.Service;

namespace WebpServer.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ScoresController : ControllerBase
    {
        private readonly IScoreService _service;

        public ScoresController(IScoreService service)
        {
            _service = service;
        }

        // 상위 랭킹 조회
        // GET /api/scores/top?limit=10
        [HttpGet("top")]
        public ActionResult<IEnumerable<ScoreProtocol>> GetTop([FromQuery] int limit = 10)
        {
            if (limit <= 0 || limit > 100)
                limit = 10;

            var result = _service.GetTop(limit);
            return Ok(result);
        }

        // 특정 플레이어 최고 점수
        // GET /api/scores/{playerId}
        [HttpGet("{playerId}")]
        public ActionResult<ScoreProtocol> GetBest(string playerId)
        {
            var dto = _service.GetBest(playerId);
            return Ok(dto);
        }

        // 점수 등록
        // POST /api/scores
        [HttpPost]
        public ActionResult<ScoreProtocol> Submit([FromBody] SubmitScoreRequest request)
        {
            var dto = _service.Submit(request);
            // 랭킹은 리소스 아이디보단 playerId 기준이라, CreatedAt으로 링크 예시
            return CreatedAtAction(nameof(GetBest), new { playerId = dto.PlayerId }, dto);
        }
    }

}

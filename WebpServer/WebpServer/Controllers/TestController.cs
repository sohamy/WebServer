using Microsoft.AspNetCore.Mvc;
using WebpServer.Exceptions;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("bad-request")]
    public IActionResult BadRequestDemo()
    {
        throw new GameValidationException("닉네임 형식이 잘못되었습니다.");
    }

    [HttpGet("unauth")]
    public IActionResult UnauthorizedDemo()
    {
        throw new GameUnauthorizedException("로그인이 필요합니다.");
    }
}

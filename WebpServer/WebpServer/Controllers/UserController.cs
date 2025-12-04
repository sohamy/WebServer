using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet]
    public string GetUsers()
    {
        return "모든 유저 목록!";
    }

    [HttpGet("detail")]
    [HttpGet("info")]
    public string Detail()
    {
        return "유저 상세정보!";
    }


    [HttpGet("search")]
    public string Search([FromQuery] string name, [FromQuery] int age)
    {
        return $"검색: name={name}, age={age}";
    }

    [HttpGet("{id}")]
    public string GetById(int id)
    {
        return $"유저 ID = {id}";
    }

    [HttpGet("{id}/inventory")]
    public string GetInventory(int id, [FromQuery] int page = 1)
    {
        return $"유저 {id}의 인벤토리 (page={page})";
    }

    [HttpGet("list")]
    public string List()
    {
        return "유저 리스트!";
    }
}

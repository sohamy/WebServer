using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebpServer.Protocol;

namespace WebpServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IValidator<CreateUserRequest> _validator;
        public UserController(IValidator<CreateUserRequest> validator)
        {
            _validator = validator;
        }

        [HttpPost("fluent")]
        public async Task<IActionResult> CreateUserWithFluentValidation([FromBody] CreateUserRequest request)
        {
            var result = await _validator.ValidateAsync(request);

            if (!result.IsValid)
            {
                // property별로 에러 묶어서 내려주는 예시
                var errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(new
                {
                    message = "Validation failed",
                    errors
                });
            }

            // TODO: 여기서부터 실제 저장 로직
            return Ok(new { message = "User created with FluentValidation" });
        }

        [HttpPost("data-annotations")]
        public IActionResult CreateUserWithDataAnnotations([FromBody] CreateUserRequest request)
        {
            // [ApiController]가 붙어 있으면,
            // ModelState가 invalid일 때 자동으로 400을 리턴해 주기도 함.
            // (커스터마이징을 위해 직접 체크하는 패턴도 알아두자)

            if (!ModelState.IsValid)
            {
                // 기본 ProblemDetails 형식으로 리턴
                return ValidationProblem(ModelState);
            }

            return Ok(new { message = "User created with DataAnnotations" });
        }

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
}
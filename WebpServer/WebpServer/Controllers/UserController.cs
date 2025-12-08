using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebpServer.Protocol;
using WebpServer.Services;
using System.Linq;

namespace WebpServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<CreateUserRequest> _createUserValidator;
        public UserController(
            IUserService userService,
            IValidator<CreateUserRequest> createUserValidator)
        {
            _userService = userService;
            _createUserValidator = createUserValidator;
        }

        [HttpPost("fluent")]
        public async Task<IActionResult> CreateUserWithFluentValidation([FromBody] CreateUserRequest request)
        {
            var result = await _createUserValidator.ValidateAsync(request);

            if (!result.IsValid)
            {
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

            // 2) 서비스에 실제 작업 위임
            var user = await _userService.CreateUserAsync(request);

            return Ok(user);
        }

        [HttpPost("data-annotations")]
        public async Task<IActionResult> CreateUserWithDataAnnotations([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var user = await _userService.CreateUserAsync(request);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync(); // Task → 실제 데이터로 꺼내기
            return Ok(users);                               // 200 OK + JSON 바디
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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
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
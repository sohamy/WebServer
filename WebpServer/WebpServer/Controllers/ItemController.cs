using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebpServer.Protocol;

namespace WebpServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IValidator<AddItemRequest> _validator;
        public ItemController(IValidator<AddItemRequest> validator)
        {
            _validator = validator;
        }


        [HttpPost("data-annotations")]
        public IActionResult AddItem([FromBody] AddItemRequest request)
        {
            // [ApiController]가 붙어 있으면,
            // ModelState가 invalid일 때 자동으로 400을 리턴해 주기도 함.
            // (커스터마이징을 위해 직접 체크하는 패턴도 알아두자)
            if (!ModelState.IsValid)
            {
                // 기본 ProblemDetails 형식으로 리턴
                return ValidationProblem(ModelState);
            }

            return new OkObjectResult(new { message = "Item added successfully" });
        }

        [HttpPost("fluent")]
        public async Task<IActionResult> CreateUserWithFluentValidation([FromBody] AddItemRequest request)
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
    }
}

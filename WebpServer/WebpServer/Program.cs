using WebpServer.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWhen(
    context => context.Request.Path.StartsWithSegments("/admin"),
    branch =>
    {
        // 이 안은 "/admin" 경로에만 적용되는 별도 파이프라인
        branch.Use(async (context, next) =>
        {
            Console.WriteLine($"[AdminOnly] {context.Request.Method} {context.Request.Path}");
            await next();
        });
    });

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<CorrelationIdMiddleware>();

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    Console.WriteLine($"[Inline] Before next: {context.Request.Method} {context.Request.Path}");

    await next(); // 다음 미들웨어로 넘기기

    Console.WriteLine($"[Inline] After next: {context.Response.StatusCode}");
});


app.UseMiddleware<LoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/hello", () => "Hello Kestrel!");

app.MapPost("/echo", (Echo echo) =>
{
    return new { Received = echo.Message };
});

app.Run();

public record Echo(string Message);

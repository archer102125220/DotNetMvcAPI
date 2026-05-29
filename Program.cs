using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// 自訂 404 頁面處理
app.UseStatusCodePages(async statusCodeContext =>
{
    var response = statusCodeContext.HttpContext.Response;

    if (response.StatusCode == 404)
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.WriteAsync($@"
            <!DOCTYPE html>
            <html lang='zh-TW'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>404 Not Found - MvcAPI</title>
                <style>
                    body {{ font-family: system-ui, -apple-system, sans-serif; text-align: center; padding: 50px; color: #333; background-color: #f4f4f9; }}
                    h1 {{ font-size: 4em; margin-bottom: 10px; color: #d9534f; }}
                    h2 {{ font-size: 1.5em; color: #555; margin-bottom: 20px; }}
                    p {{ font-size: 1.1em; color: #666; line-height: 1.6; margin-bottom: 15px; }}
                    a {{ display: inline-block; margin-top: 15px; padding: 10px 20px; color: #fff; background-color: #007bff; text-decoration: none; border-radius: 5px; font-weight: bold; transition: background-color 0.2s; }}
                    a:hover {{ background-color: #0056b3; }}
                    .container {{ max-width: 600px; margin: 0 auto; padding: 40px; border-radius: 12px; background-color: #fff; box-shadow: 0 4px 15px rgba(0,0,0,0.1); }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>404</h1>
                    <h2>找不到頁面 (Not Found)</h2>
                    <p>您好！因為這是一個 <strong>MvcAPI 專案</strong>，所以正常情況下是不會有首頁或任何前端畫面的。</p>
                    <p>若要測試 API，請前往 API 文件頁面查看可用的路由與端點：</p>
                    <a href='/scalar/v1'>👉 前往 API 文件 (Scalar UI)</a>
                </div>
            </body>
            </html>
        ");
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

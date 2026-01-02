using System;
using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware;

/// <summary>
/// 全域例外處理中介軟體，用於攔截未處理的例外並回傳統一格式的錯誤回應。
/// </summary>
/// <remarks>
/// 此中介軟體會記錄例外訊息，並根據環境（開發/生產）回傳不同詳細程度的錯誤資訊。
/// </remarks>
/// <param name="next">下一個中介軟體委派。</param>
/// <param name="logger">日誌記錄器。</param>
/// <param name="env">主機環境資訊。</param>
public class ExceptionMiddleware(RequestDelegate next,
    ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    /// <summary>
    /// 處理 HTTP 請求，並攔截任何未處理的例外。
    /// </summary>
    /// <param name="context">目前的 HTTP 上下文。</param>
    /// <returns>非同步任務。</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{message}", ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment()
                ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace ?? string.Empty)
                : new ApiException(context.Response.StatusCode, ex.Message, "網路伺服器異常");

            var option = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, option);
            await context.Response.WriteAsync(json);
        }
    }
}
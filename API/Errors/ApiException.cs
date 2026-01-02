using System;

namespace API.Errors;

/// <summary>
/// API 例外回應模型，用於統一例外處理的回應格式。
/// </summary>
/// <remarks>
/// 當伺服器發生例外時，由 <see cref="Middleware.ExceptionMiddleware"/> 使用此類別產生回應。
/// </remarks>
/// <param name="statusCode">HTTP 狀態碼。</param>
/// <param name="message">錯誤訊息。</param>
/// <param name="details">錯誤詳細資訊（開發環境顯示堆疊追蹤，生產環境顯示通用訊息）。</param>
public class ApiException(int statusCode, string message, string details)
{
    /// <summary>
    /// HTTP 狀態碼。
    /// </summary>
    /// <example>500</example>
    public int StatusCode { get; set; } = statusCode;

    /// <summary>
    /// 錯誤訊息。
    /// </summary>
    /// <example>伺服器出現錯誤，請稍後再試</example>
    public string Message { get; set; } = message;

    /// <summary>
    /// 錯誤詳細資訊。
    /// </summary>
    /// <example>網路伺服器異常</example>
    public string Details { get; set; } = details;
}
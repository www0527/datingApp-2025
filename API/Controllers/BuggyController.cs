using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// 錯誤測試控制器，用於測試各種 HTTP 錯誤狀態碼的回應。
/// </summary>
/// <remarks>
/// 此控制器僅供開發與測試使用，不應在生產環境中公開。
/// </remarks>
public class BuggyController : BaseAPIController
{
    /// <summary>
    /// 測試 401 Unauthorized 錯誤。
    /// </summary>
    /// <returns>回傳 401 未授權狀態碼。</returns>
    /// <response code="401">未授權，使用者未通過驗證。</response>
    [HttpGet("auth")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetServerAuth()
    {
        return Unauthorized();
    }

    /// <summary>
    /// 測試 404 Not Found 錯誤。
    /// </summary>
    /// <returns>回傳 404 找不到資源狀態碼。</returns>
    /// <response code="404">找不到指定的資源。</response>
    [HttpGet("not-found")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetNotFound()
    {
        return NotFound();
    }

    /// <summary>
    /// 測試 500 Internal Server Error 錯誤。
    /// </summary>
    /// <returns>拋出例外，由中介軟體處理並回傳 500 錯誤。</returns>
    /// <response code="500">伺服器內部錯誤。</response>
    /// <exception cref="Exception">模擬伺服器錯誤。</exception>
    [HttpGet("server-error")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetServerError()
    {
        throw new Exception("伺服器出現錯誤，請稍後再試");
    }

    /// <summary>
    /// 測試 400 Bad Request 錯誤。
    /// </summary>
    /// <returns>回傳 400 錯誤請求狀態碼。</returns>
    /// <response code="400">請求格式錯誤或參數無效。</response>
    [HttpGet("bad-request")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetBadRequest()
    {
        return BadRequest("這是一個錯誤的請求");
    }
}
// 用途：API控制器的基類，提供通用功能和配置，確保所有 Controller 行為一致。
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

#region 基底_API_控制器_BaseAPIController
/// <summary>
/// 所有 API 控制器的基類，統一設定路由與通用行為（例如 ModelState 驗證與輸出格式）。
/// <para>本類別透過屬性設定：</para>
/// <list type="bullet">
///   <item><term>[Route("api/[controller]")]</term><description>將路由前綴為 api/{controller}</description></item>
///   <item><term>[ApiController]</term><description>啟用 API 特有行為（自動 ModelState 驗證、綁定來源推斷、ProblemDetails 回傳等）</description></item>
/// </list>
/// </summary>
#endregion
[Route("api/[controller]")]
[ApiController]
public class BaseAPIController : ControllerBase
{
}
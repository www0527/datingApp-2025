using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    #region 成員控制器_MembersController
    /// *[controller] 會回傳目前控制器的名稱，這裡是 Members
    /// <summary>
    /// 提供成員資料的 API 控制器，回傳使用者列表與單一使用者資料。
    /// <para>建構式注入：</para>
    /// <param name="context">資料庫上下文，用於存取 AppUser 實體。</param>
    /// </summary>
    #endregion
    public class MembersController(AddDbContext context) : BaseAPIController
    {
        #region 解說
        /**
        * 使用 異步方法 提高效能
        * public async {回傳類型} {方法名稱}()
        * await {非同步操作}
        * 非同步方法必須回傳 Task 或 Task<T> 類型
        * ActionResult<T> : 表示 HTTP 回應，包含狀態碼和資料
        * IReadOnlyList<AppUser> : 只讀列表，包含 AppUser 實體

        * * 指定此方法返回一個包含 AppUser 實體的只讀列表，並且可以包含 HTTP 狀態碼。
        */
        #endregion
        #region 取得_所有成員_GetMembers
        /// <summary>
        /// 非同步取得所有使用者（只讀列表）。
        /// </summary>
        /// <returns>包含 AppUser 實體的只讀列表 (HTTP 200)；若發生例外則回傳相對應錯誤碼。</returns>
        /// <remarks>使用 Entity Framework Core 的 ToListAsync 提高非同步效能。</remarks>
        #endregion
        [HttpGet] // GET: api/members
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync(); // ToListAsync() : 非同步地將查詢結果轉換為列表

            return members;
        }

        #region 取得_單一成員_ById_GetMemberById
        /// <summary>
        /// 根據 Id 非同步取得單一使用者資料（需授權）。
        /// </summary>
        /// <param name="id">使用者唯一識別碼。</param>
        /// <returns>找到則回傳 AppUser (HTTP 200)；找不到則回傳 NotFound (HTTP 404)。</returns>
        #endregion
        [Authorize]        // 依賴 microsoft.AspNetCore.Authorization.JwtBearer 套件
        [HttpGet("{id}")]  // GET: api/members/{id}
        public async Task<ActionResult<AppUser>> GetMemberById(string id)
        {
            var member = await context.Users.FindAsync(id);

            if (member == null) return NotFound();

            return member;
        }
    }
}

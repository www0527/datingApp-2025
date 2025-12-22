using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    /** 
    * *[controller] 會回傳目前控制器的名稱，這裡是 Members
    */
    public class MembersController(AddDbContext context) : BaseAPIController
    {
        /**
        * 使用 異步方法 提高效能
        * public async {回傳類型} {方法名稱}()
        * await {非同步操作}
        * 非同步方法必須回傳 Task 或 Task<T> 類型
        * ActionResult<T> : 表示 HTTP 回應，包含狀態碼和資料
        * IReadOnlyList<AppUser> : 只讀列表，包含 AppUser 實體

        * * 指定此方法返回一個包含 AppUser 實體的只讀列表，並且可以包含 HTTP 狀態碼。
        */
        [HttpGet] // GET: api/members
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync(); // ToListAsync() : 非同步地將查詢結果轉換為列表

            return members;
        }

        [Authorize]
        [HttpGet("{id}")]  // GET: api/members/{id}
        public async Task<ActionResult<AppUser>> GetMemberById(string id)
        {
            var member = await context.Users.FindAsync(id);

            if (member == null) return NotFound();

            return member;
        }
    }
}

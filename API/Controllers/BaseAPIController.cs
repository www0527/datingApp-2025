// 用途：API控制器的基类，提供通用功能和配置，可以保證所有 Controller 的結構都相同。 
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseAPIController : ControllerBase
    {
    }
}

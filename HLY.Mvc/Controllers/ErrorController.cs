
// ***********************************************************************
// <summary>
// 异常处理页面
//</summary>
// ***********************************************************************

using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HLY.Mvc.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public string Demo()
        {
            return JsonHelper.Instance.Serialize(new Response
            {
                Code = 500,
                Message = "演示版本，不要乱动"
            });
        }

        [AllowAnonymous]
        public ActionResult Auth()
        {
            return View();
        }
    }
}
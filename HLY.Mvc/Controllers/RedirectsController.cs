using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HLY.Mvc.Controllers
{
    public class RedirectsController : Controller
    {
        /// <summary>
        /// oauth认证跳转页面
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult IdentityAuth()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/Home/Index");
            return View();
        }
    }
}

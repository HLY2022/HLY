using Microsoft.AspNetCore.Mvc;
using HLY.App.Interface;

namespace HLY.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }
       
       
        
        public ActionResult Git()
        {
            return View();
        }


        public HomeController(IAuth authUtil) : base(authUtil)
        {
        }
    }
}
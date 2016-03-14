using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForTestMvc.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// <see cref="ForTestMvc.Controllers.AccountController.Login"/>
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetNameList()
        {
            var list = new List<string>() { "张三", "李四", "王五", "赵六" };
            return Json(list);
        }

    }
}

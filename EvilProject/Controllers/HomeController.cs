using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvilProject.Models;


namespace EvilProject.Controllers
{
    public class HomeController : BaseContro
    {
        private EvilProjectEntities db = new EvilProjectEntities();

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewData.Add("News", db.PageNews.ToList().OrderBy(m => m.publish_date));
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
           // ViewBag.Message = "Your app description page.";
            
            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
           // ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

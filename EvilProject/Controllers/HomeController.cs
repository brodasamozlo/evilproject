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
        private EP_DB db = new EP_DB();
        string wyj = "";
        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                ViewData.Add("EX", "przed");


                var lista =  db.PageNewses.ToList().OrderBy(m => m.publish_date);
                if (lista != null)
                {
                    foreach (PageNews news in lista)
                    {
                        if (news.body.Length > 700)
                            news.body = news.body.Substring(0, 700) + "...";
                    }

                    ViewData.Add("News", lista);
                }
               
                ViewData["EX"] = "po";
            }
            catch (Exception ex)
            {
                wyj = ex.Message;
            }
            finally
            {
                ViewData["EX"] = wyj;
            }
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

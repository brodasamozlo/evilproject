using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvilProject.Models;

namespace EvilProject.Controllers
{
    
    public class PageNewsController : BaseContro
    {
        private EP_DB db = new EP_DB();

        //
        // GET: /PageNews/
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload(HttpPostedFileBase uploadFile, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string url; // url to return
            string message =""; // message to display (optional)
            string filePath = "";

            try
            {
                if (((System.Web.HttpRequestWrapper)(Request)).Files[0] != null)
                {

                    filePath = Path.Combine(HttpContext.Server.MapPath("~/Content/images/upload"), Path.GetFileName(((System.Web.HttpRequestWrapper)(Request)).Files[0].FileName));
                    ((System.Web.HttpRequestWrapper)(Request)).Files[0].SaveAs(filePath);
                    filePath = "/Content/images/upload/" + ((System.Web.HttpRequestWrapper)(Request)).Files[0].FileName;
                    message = "Image was saved correctly";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            url = Request.Url.GetLeftPart(UriPartial.Authority) + "/" + filePath;
            // passing message success/failure
            
            
            string output = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\", \"" + message + "\");</script></body></html>";
            return Content(output);
            //return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult Index()
        {
            return View(db.PageNewses.ToList().OrderByDescending(m=>m.publish_date));
        }

        //
        // GET: /PageNews/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id = 0)
        {
            PageNews pagenews = db.PageNewses.Find(id);
            if (pagenews == null)
            {
                return HttpNotFound();
            }
            return View(pagenews);
        }

        //
        // GET: /PageNews/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PageNews/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PageNews pagenews)
        {
            if (ModelState.IsValid)
            {
                db.PageNewses.Add(pagenews);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pagenews);
        }

        //
        // GET: /PageNews/Edit/5

        public ActionResult Edit(int id = 0)
        {
            PageNews pagenews = db.PageNewses.Find(id);
            if (pagenews == null)
            {
                return HttpNotFound();
            }
            return View(pagenews);
        }

        //
        // POST: /PageNews/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PageNews pagenews)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagenews).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pagenews);
        }

        //
        // GET: /PageNews/Delete/5

        public ActionResult Delete(int id = 0)
        {
            PageNews pagenews = db.PageNewses.Find(id);
            if (pagenews == null)
            {
                return HttpNotFound();
            }
            return View(pagenews);
        }

        //
        // POST: /PageNews/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            PageNews pagenews = db.PageNewses.Find(id);
            db.PageNewses.Remove(pagenews);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
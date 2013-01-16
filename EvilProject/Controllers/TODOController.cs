using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvilProject.Models;

namespace EvilProject.Controllers
{
    public class TODOController : Controller
    {
        private EvilProjectEntities db = new EvilProjectEntities();

        //
        // GET: /TODO/

        public ActionResult Index()
        {
            return View(db.TODO.ToList());
        }

        //
        // GET: /TODO/Details/5

        public ActionResult Details(int id = 0)
        {
            TODO todo = db.TODO.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        //
        // GET: /TODO/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TODO/Create

        [HttpPost]
        public ActionResult Create(TODO todo)
        {
            if (ModelState.IsValid)
            {
                db.TODO.Add(todo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        //
        // GET: /TODO/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TODO todo = db.TODO.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        //
        // POST: /TODO/Edit/5

        [HttpPost]
        public ActionResult Edit(TODO todo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        //
        // GET: /TODO/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TODO todo = db.TODO.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        //
        // POST: /TODO/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TODO todo = db.TODO.Find(id);
            db.TODO.Remove(todo);
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
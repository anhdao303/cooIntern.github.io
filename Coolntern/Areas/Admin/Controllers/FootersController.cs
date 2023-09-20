using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Coolntern.Filters;
using Coolntern.Help;
using Coolntern.Models;

namespace Coolntern.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class FootersController : Controller
    {
        private EntityModel db = new EntityModel();

        // GET: Admin/Footers
        public ActionResult Index()
        {
            return View(db.Footers.ToList());
        }

        // GET: Admin/Footers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Footer footer = db.Footers.Find(id);
            if (footer == null)
            {
                return HttpNotFound();
            }
            return View(footer);
        }

        // GET: Admin/Footers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Footers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,desciption,link,meta,hide,order,datebegin")] Footer footer)
        {
            Footer temp = getById(footer.id);
            if (ModelState.IsValid)
            {
                temp.name = footer.name;
                temp.desciption = footer.desciption;
                temp.link = footer.link;
                temp.meta = Functions.ConvertToUnSign(footer.name);
                temp.hide = footer.hide;
                temp.order = footer.order;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(footer);
        }

        // GET: Admin/Footers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Footer footer = db.Footers.Find(id);
            if (footer == null)
            {
                return HttpNotFound();
            }
            return View(footer);
        }

        // POST: Admin/Footers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,desciption,link,meta,hide,order,datebegin")] Footer footer, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            Footer temp = getById(footer.id);
            if (ModelState.IsValid)
            {
               
                temp.name = footer.name;
                temp.desciption = footer.desciption;
                temp.meta = Functions.ConvertToUnSign(footer.name);
                temp.hide = footer.hide;
                temp.meta = footer.meta;

                temp.order = footer.order;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(footer);
        }
        public Footer getById(long id)
        {
            return db.Footers.Where(x => x.id == id).FirstOrDefault();
        }
        // GET: Admin/Footers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Footer footer = db.Footers.Find(id);
            if (footer == null)
            {
                return HttpNotFound();
            }
            return View(footer);
        }

        // POST: Admin/Footers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var news = db.News.Where(x => x.id == id).SingleOrDefault();
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

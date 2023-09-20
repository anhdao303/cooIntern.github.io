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
    public class SlogansController : Controller
    {
        private EntityModel db = new EntityModel();

        // GET: Admin/Slogans
        public ActionResult Index()
        {
            return View(db.Slogans.ToList());
        }

        // GET: Admin/Slogans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slogan slogan = db.Slogans.Find(id);
            if (slogan == null)
            {
                return HttpNotFound();
            }
            return View(slogan);
        }

        // GET: Admin/Slogans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Slogans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,job,message,image,link,meta,hide,order,datebegin")] Slogan slogan, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";

            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/upload/img/slogan"), filename);
                    img.SaveAs(path);
                    slogan.image = filename;

                }
                else
                {
                    slogan.image = "logo.png";
                }
                slogan.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                db.Slogans.Add(slogan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slogan);
        }

        // GET: Admin/Slogans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slogan slogan = db.Slogans.Find(id);
            if (slogan == null)
            {
                return HttpNotFound();
            }
            return View(slogan);
        }

        // POST: Admin/Slogans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,job,message,image,link,meta,hide,order,datebegin")] Slogan slogan, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            Slogan temp = getById(slogan.id);
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    //filename = Guid.NewGuid().ToString() + img.FileName;
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/upload/img/slogan"), filename);
                    img.SaveAs(path);
                    temp.image = filename;
                }
                temp.name = slogan.name;
                temp.job = slogan.job;
                temp.message = slogan.message;
                temp.meta = Functions.ConvertToUnSign(slogan.name);
                temp.hide = slogan.hide;
                temp.link = slogan.link;
                temp.order = slogan.order;
                temp.datebegin = slogan.datebegin;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slogan);
        }
        public Slogan getById(long id)
        {
            return db.Slogans.Where(x => x.id == id).FirstOrDefault();
        }
        // GET: Admin/Slogans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slogan slogan = db.Slogans.Find(id);
            if (slogan == null)
            {
                return HttpNotFound();
            }
            return View(slogan);
        }

        // POST: Admin/Slogans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var slogans = db.Slogans.Where(x => x.id == id).SingleOrDefault();
            db.Slogans.Remove(slogans);
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

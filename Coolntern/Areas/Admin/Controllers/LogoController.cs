using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coolntern.Filters;
using Coolntern.Help;
using Coolntern.Models;

namespace Coolntern.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class LogoController : Controller
    {
        private EntityModel db = new EntityModel();

        // GET: Admin/Logo
        public ActionResult Index()
        {
            return View(db.Logoes.ToList());
        }

        // GET: Admin/Logo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logo logo = db.Logoes.Find(id);
            if (logo == null)
            {
                return HttpNotFound();
            }
            return View(logo);
        }

        // GET: Admin/Logo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Logo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,image,link,meta,hide,order,datebegin")] Logo logo, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";

            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/upload/img/logo"), filename);
                    img.SaveAs(path);
                    logo.image = filename;

                }
                else
                {
                    logo.image = "logo.png";
                }
                logo.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                db.Logoes.Add(logo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(logo);
        }

        // GET: Admin/Logo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logo logo = db.Logoes.Find(id);
            if (logo == null)
            {
                return HttpNotFound();
            }
            return View(logo);
        }

        // POST: Admin/Logo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,image,link,meta,hide,order,datebegin")] Logo logo, HttpPostedFileBase img)
        {

            var path = "";
            var filename = "";
            Logo temp = getById(logo.id);
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    //filename = Guid.NewGuid().ToString() + img.FileName;
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/upload/img/logo"), filename);
                    img.SaveAs(path);
                    temp.image = filename;
                }
                temp.name = logo.name;
                temp.link = logo.meta;
                temp.meta = Functions.ConvertToUnSign(logo.name);
                temp.hide = logo.hide;
                temp.order = logo.order;
                temp.datebegin = logo.datebegin;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                return View(logo);
            
        }

            // GET: Admin/Logo/Delete/5
            public Logo getById(long id)
            {
                return db.Logoes.Where(x => x.id == id).FirstOrDefault();
            }
            public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logo logo = db.Logoes.Find(id);
            if (logo == null)
            {
                return HttpNotFound();
            }
            return View(logo);
        }

        // POST: Admin/Logo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var logo = db.Logoes.Where(x => x.id == id).SingleOrDefault();
            db.Logoes.Remove(logo);
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

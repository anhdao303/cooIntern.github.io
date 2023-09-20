using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Coolntern.Filters;
using Coolntern.Help;
using Coolntern.Models;

namespace Coolntern.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class BannersController : Controller
    {
        private EntityModel db = new EntityModel();

        // GET: Admin/Banners
        public ActionResult Index()
        {
            return View(db.Banners.ToList());
        }

        // GET: Admin/Banners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // GET: Admin/Banners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,message,image,link,meta,hide,order,datebegin")] Banner banner, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";

            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/upload/img/banner"), filename);
                    img.SaveAs(path);
                    banner.image = filename;

                }
                else
                {
                    banner.image = "logo.png";
                }
                banner.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                db.Banners.Add(banner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(banner);
        }

        // GET: Admin/Banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Admin/Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,message,image,link,meta,hide,order,datebegin")] Banner banner, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            Banner temp = getById(banner.id);
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    //filename = Guid.NewGuid().ToString() + img.FileName;
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/upload/img/banner"), filename);
                    img.SaveAs(path);
                    temp.image = filename;
                }
                temp.name = banner.name;
                temp.message = banner.message;
                temp.link = banner.link;
                temp.meta = Functions.ConvertToUnSign(banner.name);
                temp.hide = banner.hide;
                temp.order = banner.order;
                temp.datebegin = banner.datebegin;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banner);
        }

        // GET: Admin/Banners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Admin/Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var banner = db.Banners.Where(x => x.id == id).SingleOrDefault();
            db.Banners.Remove(banner);
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

        public Banner getById(long id)
        {
            return db.Banners.Where(x => x.id == id).FirstOrDefault();
        }
    }
}

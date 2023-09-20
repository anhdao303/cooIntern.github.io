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
    public class NCategoriesController : Controller
    {
        private EntityModel db = new EntityModel();

        // GET: Admin/NCategories
        public ActionResult Index()
        {
            return View(db.NCategories.ToList());
        }

        // GET: Admin/NCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NCategory nCategory = db.NCategories.Find(id);
            if (nCategory == null)
            {
                return HttpNotFound();
            }
            return View(nCategory);
        }

        // GET: Admin/NCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,desciption,link,meta,hide,order,datebegin")] NCategory nCategory)
        {
            if (ModelState.IsValid)
            {
                nCategory.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                db.NCategories.Add(nCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nCategory);
        }

        // GET: Admin/NCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NCategory nCategory = db.NCategories.Find(id);
            if (nCategory == null)
            {
                return HttpNotFound();
            }
            return View(nCategory);
        }

        // POST: Admin/NCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,desciption,link,meta,hide,order,datebegin")] NCategory nCategory)
        {

          
            NCategory temp = getById(nCategory.id);
            if (ModelState.IsValid)
            {
                temp.name = nCategory.name;
                temp.desciption = nCategory.desciption;
                temp.link = nCategory.link;
                temp.meta = Functions.ConvertToUnSign(nCategory.name);
                temp.hide = nCategory.hide;
                temp.order = nCategory.order;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nCategory);
        }
       

        // GET: Admin/NCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NCategory nCategory = db.NCategories.Find(id);
            if (nCategory == null)
            {
                return HttpNotFound();
            }
            return View(nCategory);
        }

        // POST: Admin/NCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NCategory nCategory = db.NCategories.Find(id);
            db.NCategories.Remove(nCategory);
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
      public NCategory getById(int id)
        {
            return db.NCategories.Where(x => x.id == id).FirstOrDefault();
        }
    }
}

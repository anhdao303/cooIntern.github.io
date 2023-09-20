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
    public class JobsController : Controller
    {
        private EntityModel db = new EntityModel();

        // GET: Admin/Jobs
        public ActionResult Index(long? id = null)
        {
            getCategory(id);
            //return View(db.products.ToList());
            return View();
        }
        public void getCategory(long? selectedId = null)
        {
            ViewBag.Category = new SelectList(db.Categories.Where(x => x.hide == true)
                .OrderBy(x => x.order), "id", "name", selectedId);
        }
        public ActionResult getJob(long? id)
        {
            if (id == null)
            {
                var v = db.Jobs.OrderBy(x => x.order).ToList();
                return PartialView(v);
            }
            var m = db.Jobs.Where(x => x.categoryId == id).OrderBy(x => x.order).ToList();
            return PartialView(m);
        }
        // GET: Admin/Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Admin/Jobs/Create
        public ActionResult Create()
        {
            ViewBag.categoryId = new SelectList(db.Categories, "id", "name");
            return View();
        }

        // POST: Admin/Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public ActionResult Create([Bind(Include = "id,name,nameCompany,email,salary,location,vacancy,image,description,requirement,link,meta,hide,order,dateapply,datebegin,categoryId")] 
        Job job, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";

            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/upload/img/job"), filename);
                    img.SaveAs(path);
                    job.image = filename;

                }
                else
                {
                    job.image = "logo.png";
                }
                job.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryId = new SelectList(db.Categories, "id", "name", job.categoryId);
            return View(job);
        }

        // GET: Admin/Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryId = new SelectList(db.Categories, "id", "name", job.categoryId);
            return View(job);
        }

        // POST: Admin/Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]

        public ActionResult Edit([Bind(Include = "id,name,nameCompany,email,salary,location,vacancy,image,description,requirement,link,meta,hide,order,dateapply,datebegin,categoryId")] Job job, HttpPostedFileBase img)
        {
            var path = "";
            var filename = "";
            Job temp = getById(job.id);
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    //filename = Guid.NewGuid().ToString() + img.FileName;
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/upload/img/job"), filename);
                    img.SaveAs(path);
                    temp.image = filename;
                }
                temp.name = job.name;
                temp.nameCompany = job.nameCompany;
                temp.email = job.email;
                temp.description = job.description;
                temp.location= job.location;
                temp.vacancy= job.vacancy;
                temp.requirement= job.requirement;
                temp.datebegin= job.datebegin;
                temp.dateapply= job.dateapply;
                temp.categoryId = temp.categoryId;
                temp.meta = Functions.ConvertToUnSign(job.name);
                temp.hide = job.hide;
                temp.order = job.order;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }
        public Job getById(long id)
        {
            return db.Jobs.Where(x => x.id == id).FirstOrDefault();
        }

        // GET: Admin/Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Admin/Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var job = db.Jobs.Where(x => x.id == id).SingleOrDefault();
            db.Jobs.Remove(job);
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

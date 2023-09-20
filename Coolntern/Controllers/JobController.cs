using Coolntern.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coolntern.Filters;
using Microsoft.AspNet.Identity;

namespace Coolntern.Controllers
{
    public class JobController : Controller
    {
        EntityModel _db = new EntityModel();

        // GET: Job
        public ActionResult Index(string meta)
        {
            var key = Request.QueryString["key"];
            var location = Request.QueryString["location"];

            if(key != null || location != null)
            {
                ViewBag.isAllJob = false;
                ViewBag.searchJob = true;
                ViewBag.key = key;
                ViewBag.location = location;

                return View();
            }

            ViewBag.searchJob = false;
            if (meta == null)
            {
                ViewBag.isAllJob = true;
                return View();
            }
            else
            {
                ViewBag.isAllJob = false;
                

                var job = _db.Categories.Where(_ => _.meta == meta);

                return View(job.FirstOrDefault());
            }
        }

        public ActionResult GetJobBySearchKey(string key, string location)
        {
            ViewBag.meta = "cong-viec";

            var job = _db.Jobs
                .Where(_ => _.name.ToLower().Contains(key.ToLower()) 
                    || 
                    _.location.ToLower().Contains(location.ToLower()))
                .ToList();

            return PartialView(job);
        }

        public ActionResult GetJobByCategory(long? id, string metatitle) 
        {
            ViewBag.meta = "cong-viec";

            if(id == null)
            {
                var jobs = _db.Jobs.Where(_ => _.hide == true).OrderBy(_ => _.datebegin);

                return PartialView(jobs.ToList());
            }

            var job = _db.Jobs
                .Where(_ => _.categoryId == id)
                .Where(_ => _.hide == true)
                .OrderBy(_ => _.datebegin);

            return PartialView(job.ToList());
        }

        public ActionResult Detail(string meta)
        {
            ViewBag.meta = "cong-viec";
            ViewBag.HadApply = false;

            if (meta == null)
            {
                return RedirectToAction("Index", "Job");
            }

            var job = _db.Jobs.Where(_ => _.meta == meta).FirstOrDefault();

            var curUserId = User.Identity.GetUserId();

            if (curUserId != null)
            {
                var jobDetail = _db.DetailJobs
                    .Where(_ => _.id_job == job.id)
                    .Where(_ => _.id_user == curUserId)
                    .FirstOrDefault();

                if(jobDetail != null)
                {
                    ViewBag.HadApply = true;
                }    
            }

            return View(job);
        }

        [MyAuthentication]
        public ActionResult ApplyJob(string meta)
        {
            var curUserId = User.Identity.GetUserId();

            var job = _db.Jobs.Where(_ => _.meta == meta).FirstOrDefault();

            var _detailJob = new DetailJob()
            {
                id_job = job.id,
                id_user = curUserId,
                approval = (short) JobState.WaitApprove,
                dateApply = DateTime.Now,
            };

            var tempJob = _db.DetailJobs
                .Where(_ => _.id_job == job.id)
                .Where(_ => _.id_user == curUserId)
                .FirstOrDefault();

            if(tempJob != null)
            {
                return Redirect("/cong-viec");
            }

            _db.DetailJobs.Add(_detailJob);
            _db.SaveChanges();

            return Redirect("/cong-viec-cua-ban");
        }
    }
}
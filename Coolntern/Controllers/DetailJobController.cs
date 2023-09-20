using Coolntern.ViewModel;
using Coolntern.Filters;
using Coolntern.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coolntern.Controllers
{
    [MyAuthentication]
    public class DetailJobController : Controller
    {
        EntityModel _db = new EntityModel();

        public ActionResult GetJobApply()
        {
            var jobs = _db.DetailJobs.ToList();

            return View(jobs);
        }

        [ChildActionOnly]
        public ActionResult GetSideBar()
        {
            return PartialView();
        }

        public ActionResult GetAllJobApply()
        {
            ViewBag.meta = "cong-viec";

            var userId = User.Identity.GetUserId();

            var job = _db.DetailJobs
                        .Where(_ => _.id_user == userId)
                        .Where(_ => _.Job.hide == true)
                        .ToList();

            return PartialView(job);
        }

        public ActionResult GetAllJobApproval()
        {
            ViewBag.meta = "cong-viec";

            var userId = User.Identity.GetUserId();

            var job = _db.DetailJobs
                        .Where(_ => _.id_user == userId)
                        .Where(_ => _.approval == (short) JobState.Approve)
                        .Where(_ => _.Job.hide == true)
                        .ToList();

            return PartialView(job);
        }

        public ActionResult GetAllJobWaitApproval()
        {
            ViewBag.meta = "cong-viec";

            var userId = User.Identity.GetUserId();

            var job = _db.DetailJobs
                        .Where(_ => _.id_user == userId)
                        .Where(_ => _.approval == (short) JobState.WaitApprove)
                        .Where(_ => _.Job.hide == true)
                        .ToList();

            return PartialView(job);
        }

        public ActionResult GetJobDeny()
        {
            ViewBag.meta = "cong-viec";

            var userId = User.Identity.GetUserId();

            var job = _db.DetailJobs
                .Where(_ => _.id_user == userId)
                .Where(_ => _.approval == (short) JobState.Deny)
                .Where(_ => _.Job.hide == true)
                .ToList();

            return PartialView(job);
        }

        [HttpPost]
        public ActionResult GetAcceptJob()
        {
            var jobId = Int32.Parse(Request.Form["jobId"]);
            var userId = User.Identity.GetUserId();

            var _detailJob = (
                from detailJob in _db.DetailJobs
                join user in _db.AspNetUsers on detailJob.id_user equals user.Id
                join job in _db.Jobs on detailJob.id_job equals job.id
                where user.Id == userId && job.id == jobId
                select new ApplyJob
                {
                    userId = detailJob.id_user,
                    jobId = detailJob.id_job,
                    JobState = detailJob.approval,
                    DateApply = detailJob.dateApply,
                    Message = detailJob.message,
                    NameJob = job.name,
                    NameCompany = job.nameCompany,
                    LocationJob = job.location,
                    ImageJob = job.image,
                    Requirement = job.requirement,
                    Description = job.description,
                    Salary = job.salary,
                }
            ).FirstOrDefault();

            if (_detailJob == null)
            {
                return Redirect("/cong-viec-cua-ban");
            }

            return View(_detailJob);
        }

        [HttpPost]
        public ActionResult GetDenyJob()
        {
            var jobId = Int32.Parse(Request.Form["jobId"]);
            var userId = User.Identity.GetUserId();

            var detailJob = _db.DetailJobs
                .Where(_ => _.id_user == userId && _.id_job == jobId)
                .FirstOrDefault();

            if (detailJob == null)
            {
                return Redirect("/cong-viec-cua-ban");
            }

            return View(detailJob);
        }
    }
}
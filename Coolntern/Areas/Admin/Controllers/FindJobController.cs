using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using Coolntern.ViewModel;
using Coolntern.Filters;
using Coolntern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coolntern.Models;
using System.Data.Entity.Migrations;

namespace Coolntern.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class FindJobController : Controller
    {
        private EntityModel db = new EntityModel();

        // GET: Admin/FindJob
        public ActionResult Index()
        {
            var listApplyJobs = (
                from detailJob in db.DetailJobs
                join user in db.AspNetUsers on detailJob.id_user equals user.Id
                join job in db.Jobs on detailJob.id_job equals job.id
                where detailJob.approval == (short) JobState.WaitApprove
                select new ApplyJob
                {
                    userId = detailJob.id_user,
                    jobId = detailJob.id_job,
                    UserName = user.Name,
                    GPA = user.GPA,
                    TrainingPoint = user.TrainingPoint,
                    JobName = job.name,
                    JobState = detailJob.approval,
                    DateApply = detailJob.dateApply,
                }
            ).ToList();

            return View(listApplyJobs);
        }

        public ActionResult AcceptJob()
        {
            var listApplyJobs = (
                from detailJob in db.DetailJobs
                join user in db.AspNetUsers on detailJob.id_user equals user.Id
                join job in db.Jobs on detailJob.id_job equals job.id
                where detailJob.approval == (short) JobState.Approve
                select new ApplyJob
                {
                    userId = detailJob.id_user,
                    jobId = detailJob.id_job,
                    UserName = user.Name,
                    GPA = user.GPA,
                    TrainingPoint = user.TrainingPoint,
                    JobName = job.name,
                    JobState = detailJob.approval,
                    DateApply = detailJob.dateApply,
                }
            ).ToList();

            return View(listApplyJobs);
        }

        public ActionResult DenyJob()
        {
            var listApplyJobs = (
                from detailJob in db.DetailJobs
                join user in db.AspNetUsers on detailJob.id_user equals user.Id
                join job in db.Jobs on detailJob.id_job equals job.id
                where detailJob.approval == (short)JobState.Deny
                select new ApplyJob
                {
                    userId = detailJob.id_user,
                    jobId = detailJob.id_job,
                    UserName = user.Name,
                    GPA = user.GPA,
                    TrainingPoint = user.TrainingPoint,
                    JobName = job.name,
                    JobState = detailJob.approval,
                    DateApply = detailJob.dateApply,
                }
            ).ToList();

            return View(listApplyJobs);
        }

        public ActionResult Accept(string userId, int jobId)
        {
            var userApply = (
                from detailJob in db.DetailJobs
                join user in db.AspNetUsers on detailJob.id_user equals user.Id
                join job in db.Jobs on detailJob.id_job equals job.id
                where user.Id == userId && job.id == jobId
                select new ApplyJob
                {
                    userId = detailJob.id_user,
                    jobId = detailJob.id_job,
                    UserName = user.Name,
                    GPA = user.GPA,
                    Avatar = user.Avatar,
                    Address = user.Address,
                    Email = user.Email,
                    TrainingPoint = user.TrainingPoint,
                    JobState = detailJob.approval,
                    DateApply = detailJob.dateApply,
                }
            ).FirstOrDefault();

            return View(userApply);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ConfirmAcceptJob()
        {
            var _jobId = Int32.Parse(Request.Form["jobId"]);
            var _userId = Request.Form["userId"];
            var message = Request.Unvalidated.Form["Message"];

            var currApplyJob = db.DetailJobs
                .Where(_ => _.id_job == _jobId)
                .Where(_ => _.id_user == _userId)
                .FirstOrDefault();

            if(currApplyJob == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                currApplyJob.approval = (short)JobState.Approve;
                currApplyJob.message = message;

                db.Entry(currApplyJob).State = EntityState.Modified;
                db.SaveChanges();
            }  
            catch(Exception ex)
            {

            }

            return RedirectToAction("AcceptJob");
        }

        public ActionResult DetailAccept(string userId, int jobId)
        {
            var userApply = (
                from detailJob in db.DetailJobs
                join user in db.AspNetUsers on detailJob.id_user equals user.Id
                join job in db.Jobs on detailJob.id_job equals job.id
                where user.Id == userId && job.id == jobId
                select new ApplyJob
                {
                    userId = detailJob.id_user,
                    jobId = detailJob.id_job,
                    UserName = user.Name,
                    GPA = user.GPA,
                    Avatar = user.Avatar,
                    Address = user.Address,
                    Email = user.Email,
                    TrainingPoint = user.TrainingPoint,
                    JobState = detailJob.approval,
                    DateApply = detailJob.dateApply,
                    Message = detailJob.message,
                }
            ).FirstOrDefault();

            return View(userApply);
        }

        [HttpPost]
        public ActionResult Deny()
        {
            var _jobId = Int32.Parse(Request.Form["jobId"]);
            var _userId = Request.Form["userId"];
            var message = Request.Unvalidated.Form["Message"];

            var currApplyJob = db.DetailJobs
                .Where(_ => _.id_job == _jobId)
                .Where(_ => _.id_user == _userId)
                .FirstOrDefault();

            if (currApplyJob == null)
            {
                return RedirectToAction("Index");
            }

            currApplyJob.approval = (short)JobState.Deny;
            currApplyJob.message = message;

            //save change
            db.Entry(currApplyJob).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("DenyJob");
        }
    }
}
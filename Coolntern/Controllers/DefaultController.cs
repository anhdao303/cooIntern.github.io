using Coolntern.Models;
using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;

namespace Coolntern.Controllers
{
    public class DefaultController : Controller
    {
        EntityModel _db = new EntityModel();

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getLastNews()
        {
            ViewBag.meta = "thong-bao";
            var news = _db.News
                .Where(_ => _.hide == true)
                .OrderByDescending(_ => _.datebegin)
                .Take(2);

            return PartialView(news.ToList());
        }

        public ActionResult getSlogan()
        {
            var slogan = _db.Slogans.Where(_ => _.hide == true).OrderByDescending(_ => _.order);

            return PartialView(slogan.ToList());
        }

        public ActionResult getBanner()
        {
            var banner = _db.Banners
                .Where(_ => _.hide == true)
                .OrderByDescending(_ => _.order)
                .FirstOrDefault();

            return PartialView(banner);
        }

        public ActionResult getCategory()
        {
            ViewBag.meta = "cong-viec";
            var category = _db.Categories.Where(_ => _.hide == true).OrderByDescending(_ => _.order);

            return PartialView(category.ToList());
        }

        public ActionResult getProcess()
        {
            var process = _db.Processes.Where(_ => _.hide == true).OrderBy(_ => _.order);

            return PartialView(process.ToList());
        }

        public ActionResult getCurrentJob(Boolean isFull) 
        {
            ViewBag.meta = "cong-viec";

            IEnumerable job = default;

            if (isFull)
            {
                job = _db.Jobs.Where(_ => _.hide == true).OrderBy(_ => _.datebegin).ToList();
            }
            else
            {
                job = _db.Jobs.Where(_ => _.hide == true).OrderBy(_ => _.datebegin).Take(5).ToList();
            }

            return PartialView(job);
        }
    }
}
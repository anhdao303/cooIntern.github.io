using Coolntern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coolntern.Controllers
{
    public class NewsController : Controller
    {
        EntityModel _db = new EntityModel();

        // GET: News
        public ActionResult Index(string meta)
        {
            if(meta != null)
            {
                ViewBag.isAllNews = false;

                var news = _db.NCategories.Where(_ => _.meta== meta).FirstOrDefault();

                return View(news);
            }
            else
            {
                ViewBag.isAllNews = true;

                return View();
            }
        }

        public ActionResult GetNewsByCategory(long id, string metatitle)
        {
            ViewBag.meta = "thong-bao";

            var job = _db.News.Where(_ => _.categoryId == id).Where(_ => _.hide == true).OrderBy(_ => _.datebegin).Take(5);

            return PartialView(job.ToList());
        }

        public ActionResult getCurrentNews()
        {
            ViewBag.meta = "thong-bao";
            var currNews = _db.News.OrderByDescending(_ => _.order).Take(5).ToList();

            return PartialView(currNews);
        }

        public ActionResult getNews()
        {
            ViewBag.meta = "thong-bao";
            var currNews = _db.News.OrderByDescending(_ => _.order).Take(5).ToList();

            return PartialView(currNews);
        }

        public ActionResult Detail(string meta)
        {
            ViewBag.meta = "thong-bao";

            var news = _db.News.Where(_ => _.meta == meta).FirstOrDefault();

            return View(news);
        }
    }
}
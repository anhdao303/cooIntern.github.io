using Coolntern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coolntern.Controllers
{
    public class TempController : Controller
    {
        // GET: Temp
        public ActionResult Index()
        {
            return View();
        }

        EntityModel _db = new EntityModel();

        public ActionResult getFullHeader()
        {
            return PartialView();
        }

        public ActionResult getLogo()
        {
            var logo = _db.Logoes;

            return PartialView(logo.FirstOrDefault());
        }

        public ActionResult getMenu()
        {
            var v = _db.Menus.Where(m => m.hide == true).OrderByDescending(m => m.order);

            return PartialView(v.ToList());
        }

        public ActionResult getFooter()
        {
            var footer = _db.Footers.Where(f => f.hide == true).OrderByDescending(_ => _.order);

            return PartialView(footer.ToList());
        }

        public ActionResult getSideBarJob()
        {
            ViewBag.meta = "cong-viec";

            var c = _db.Categories.Where(_ => _.hide == true).ToList();

            return PartialView(c);
        }

        public ActionResult getNewsCategory()
        {
            ViewBag.meta = "thong-bao";

            var c = _db.NCategories.Where(_ => _.hide == true).ToList();

            return PartialView(c);
        }
    }
}
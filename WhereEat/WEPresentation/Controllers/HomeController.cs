using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;
using WEPresentation.Models;

namespace WEPresentation.Controllers
{
    public class HomeController : Controller
    {
        WEContext db = new WEContext();
        public ActionResult Index()
        {
            bool hasLog = db.Logs.Any(l => DbFunctions.TruncateTime(l.Date) == DbFunctions.TruncateTime(l.Date));

            Configuration config = db.Configurations.FirstOrDefault();
            if (config.NiceTryMode && hasLog)
                ViewBag.NiceTryMode = config.NiceTryMode;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
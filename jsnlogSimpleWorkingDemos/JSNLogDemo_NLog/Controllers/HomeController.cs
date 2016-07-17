using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JSNLogDemo_NLog.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
		
NLog.Logger logger = NLog.LogManager.GetLogger("serverlogger");
logger.Info("info server log message");

            return View();
        }
    }
}




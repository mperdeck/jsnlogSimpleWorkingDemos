using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Serilog;

namespace EmptySerilog_MongoDb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Log.Logger.Information("Logging from controller2");

            //######################
            var d0 = new { x = 5, y = 88 };
            var d1 = new { z = 9, d0 = d0 };
            Log.Logger.Information("non at logging {d1}", d1);
            Log.Logger.Information("at logging {@d1}", d1);

            return View();
        }
    }
}

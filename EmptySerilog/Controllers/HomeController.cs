using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Serilog;

namespace EmptySerilog.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Log.Logger.Information("Hello, Serilog2!");

            var d1 = new { x = 5, y = 88 };
            Log.Logger.Information("{d1}", d1);
            Log.Logger.Information("{@d1}", d1);

            return View();
        }
    }
}

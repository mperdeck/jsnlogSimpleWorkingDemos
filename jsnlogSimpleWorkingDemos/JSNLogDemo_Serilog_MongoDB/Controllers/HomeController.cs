using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JSNLogDemo_Serilog_MongoDB.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
		
Serilog.Log.Logger.Information("info server log message");

            return View();
        }
    }
}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JSNLogDemo_Elmah.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
		
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("info server log message"));

            return View();
        }
    }
}




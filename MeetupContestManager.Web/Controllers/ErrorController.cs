using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MeetupContestManager.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View("Error");
        }

    }
}

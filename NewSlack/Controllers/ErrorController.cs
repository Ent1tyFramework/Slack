using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Slack.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;

            return View();
        }
        public ActionResult ProccessError()
        {
            Response.StatusCode = 404;

            return View("Error");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace NetworkMarketing.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult AllbankDetails()
        {
            return View();
        }
    }
}

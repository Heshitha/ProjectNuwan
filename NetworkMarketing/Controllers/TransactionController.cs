using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetworkMarketing.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult TransactionHistory()
        {
            return View();
        }
    }
}
using NetworkDataAccess;
using NetworkMarketing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetworkMarketing.Controllers
{
    public class FinancialController : Controller
    {
        // GET: Financial
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TransferPoints()
        {
                return View();
        }

        public ActionResult FrmTransferPoints(string TransactionKey)
        {
            User usr = (User)Session["User"];
            if (usr.TransctionKey == TransactionKey)
            {
                return View();
            }
            else
            {
                Response.Redirect("#/Financial/TransferPoints");
                return View("TransferPoints");
            }
        }

        public ActionResult TransactionCode()
        {
            return View();
        }

        public ActionResult FinancialManager(string TransactionKey = "")
        {
            //var tmv = TransactionKeyMV;
            //string TransactionKey = Request.QueryString["TransactionKey"];
            User usr = (User)Session["User"];
            if (usr.TransctionKey == TransactionKey)
            {
                return View();
            }
            else
            {
                Response.Redirect("#/Financial/TransactionCode");
                return View("/transactionCode");
            }

        }
    }
}
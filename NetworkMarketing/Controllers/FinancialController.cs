using NetworkBussiness;
using NetworkDataAccess;
using NetworkMarketing.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpPost]
        public JsonResult UploadProof(string imageID,int ID, HttpPostedFileBase file)
        {
            int retVal = 0;
            try
            {
                string path = Server.MapPath("~") + "//Uploads//BankProofs//" + imageID + Path.GetExtension(file.FileName);
                file.SaveAs(path);

                string savePath = "/Uploads/BankProofs/" + imageID + Path.GetExtension(file.FileName);

                retVal = NetworkBussiness.FinancialManager.uploadProof(savePath, ID);
            }
            catch (Exception ex)
            {

            }
            return Json(retVal, JsonRequestBehavior.AllowGet);
        }

    }
}
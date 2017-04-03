using NetworkBussiness;
using NetworkMarketing.Models;
using NetworkModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetworkMarketing.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.IsError = false;
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            ViewBag.IsError = false;
            ViewBag.Error = "";
            try
            {

                int result = UserManager.LoginUser(username, password);
                if (result == 0)
                {
                    ViewBag.IsError = true;
                    ViewBag.Error = "User does not exists. Please check username and password!";
                }
                else if (result == -1)
                {
                    ViewBag.IsError = true;
                    ViewBag.Error = "Password invalid. Please check username and password!";
                }
                else
                {
                    Session["User"] = UserManager.GetUserDetails(result);
                    Response.Redirect("~/Home/HomePage", false);
                }
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
                ViewBag.IsError = true;
                ViewBag.Error = "Oops!, Something went wrong. Please try again.";
            }
            return View();
        }

        [HttpPost]
        public bool SignOut()
        {
            bool retVal = false;
            try
            {
                if (Session["User"] != null)
                {
                    Session.Abandon();
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
            }
            return retVal;
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            ViewBag.IsError = false;
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpModel signUp)
        {
            ViewBag.IsError = false;
            ViewBag.Error = "";
            try
            {
                int result = UserManager.SignUpUser(signUp);
                if (result == 0)
                {
                    ViewBag.IsError = true;
                    ViewBag.Error = "Oops!, Something went wrong. Please try again.";
                }
                else if(result == -1)
                {
                    ViewBag.IsError = true;
                    ViewBag.Error = "Looks like you have entered the wrong Sponsor username. Please check and try again.";
                }
                else if (result == -2)
                {
                    ViewBag.IsError = true;
                    ViewBag.Error = "Looks like the class you have entered does not exists or already filled out. Please check and try again.";
                }
                else if (result == -3)
                {
                    ViewBag.IsError = true;
                    ViewBag.Error = "The username you have entered already exists. Please change and try again.";
                }
                else if (result == -4)
                {
                    ViewBag.IsError = true;
                    ViewBag.Error = "Looks like the E Voucher you have entered does not exists or already used. Please check and try again.";
                }
                else
                {
                    Session["User"] = UserManager.GetUserDetails(result);
                    Response.Redirect("~/Home/HomePage", false);
                }
            }
            catch (Exception ex)
            {
                LogClass.WriteErrorLog(ex);
                ViewBag.IsError = true;
                ViewBag.Error = "Oops!, Something went wrong. Please try again.";
            }
            return View();
        }

        public ActionResult MyProfile()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveProfileImage(int userID, HttpPostedFileBase file)
        {
            bool retVal = false;
            try
            {
                string path = Server.MapPath("~") + "//Uploads//ProfileImages//" + userID + Path.GetExtension(file.FileName);
                file.SaveAs(path);
                NetworkDataAccess.User usr = new NetworkDataAccess.User()
                {
                    UserID = userID,
                    ImageExt = Path.GetExtension(file.FileName)
                };

                retVal = UserManager.SaveImageExtension(usr);
                if (retVal)
                {
                    usr = (NetworkDataAccess.User)Session["User"];
                    usr.ImageExt = Path.GetExtension(file.FileName);
                    Session["User"] = usr;
                }
            }
            catch (Exception ex)
            {

            }
            return Json(retVal, JsonRequestBehavior.AllowGet);
        }
	}
}
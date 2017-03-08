using NetworkBussiness;
using NetworkMarketing.Models;
using System;
using System.Collections.Generic;
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
                    Response.Redirect("~/", false);
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

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult MyProfile()
        {
            return View();
        }
	}
}
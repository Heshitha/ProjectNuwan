using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetworkMarketing.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ExampleHomePage()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult ViewClass()
        {
            NetworkDataAccess.User user = (NetworkDataAccess.User)Session["User"];
            var result = NetworkDataAccess.ClassDataAccess.GetClassHistory(user.UserID);
            string classList = "[";
            for (int i = 0; i < result.Length; i++)
            {
                classList += result[i].ToString();
                if (i < result.Length - 1)
                {
                    classList += ", ";
                }
            }
            classList += "]";
            ViewBag.ClassList = classList;
            return View();
        }

        public ActionResult ViewTeam()
        {
            return View();
        }

        public ActionResult EVouchers()
        {
            return View();
        }

        public ActionResult MyEVouchers()
        {
            return View();
        }

        public ActionResult EVoucherDetails()
        {
            return View();
        }

        public ActionResult TransactionHistory()
        {
            return View();
        }
    }
}
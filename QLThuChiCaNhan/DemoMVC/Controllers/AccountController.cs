using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoMVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {

            return View();
        }
        public ActionResult Register()
        {

            return View();
        }
        public ActionResult RegisterAccount()
        {

            return View();
        }
        public ActionResult RegisterResult()
        {

            return RedirectToAction("Login", "Account");
        }
        public ActionResult Logout()
        {

            return RedirectToAction("Login", "Account");
        }
    }
}
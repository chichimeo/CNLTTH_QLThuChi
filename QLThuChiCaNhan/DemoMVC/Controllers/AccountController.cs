using DemoMVC.DangNhap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoMVC.Controllers
{
    
    public class AccountController : Controller
    {
        private TuTuService service = new TuTuService();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var user = service.KiemTraDN(username, password);
                if(user != null)
                {
                    Session["User"] = user;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Đăng nhập thất bại";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        public ActionResult Register()
        {

            return View();
        }
        public ActionResult RegisterResult()
        {

            return View();
        }
        public ActionResult RegisterAccount(string name, string email, string username, string password)
        {
            if(service.ThemNguoiDung(name, email, username, password))
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.error = "Đăng ký thất bại";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
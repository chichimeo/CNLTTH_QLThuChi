using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMVC.SoChiTieu;
using DemoMVC.DangNhap;

namespace DemoMVC.Controllers
{
    public class SoChiTieuController : Controller
    {

        private ChiChiService service = new ChiChiService();
        // GET: SoChiTieu
        public ActionResult ChiTieu(string SearchString)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = (NguoiDung)Session["User"];
            
            DateTime now = DateTime.Now;
            string mathang = now.Year.ToString() + "-" + now.Month.ToString();
            //string mathang = "2020-10";
            List<CTKhoanChiCD> khoanchicd = service.KhoanChiCoDinh(mathang, user.mand).ToList();
            List<CTKhoanChi> khoanchi = service.KhoanChiTheoThang(mathang, user.mand).ToList();
            List<CTKhoanThu> khoanthu = service.KhoanThuTheoThang(mathang, user.mand).ToList();
            List<CTKHoanThuCD> khoanthucd = service.KhoanThuCoDinh(mathang, user.mand).ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                 khoanchicd = service.SearchKhoanChiCoDinh(mathang,SearchString, user.mand).ToList();
                 khoanchi = service.SearchKhoanChiTheoThang(mathang, SearchString, user.mand).ToList();
                 khoanthu = service.SearchKhoanThuTheoThang(mathang, SearchString, user.mand).ToList();
                 khoanthucd = service.SearchKhoanThuCoDinh(mathang, user.mand, SearchString).ToList();
            }
            ViewBag.khoanchicd = khoanchicd;
            ViewBag.khoanchi = khoanchi;
            ViewBag.khoanthucd = khoanthucd;
            ViewBag.khoanthu = khoanthu;
            return View();
        }

        public ActionResult AddEditKhoanChiCD(int itemId)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = (NguoiDung)Session["User"];

            CTKhoanChiCD model = new CTKhoanChiCD();
            if (itemId > 0)
            {
                model = service.kccd(itemId, user.mand);
            }
            ViewBag.khoanchi = model;
            List<LoaiKhoanChi> list = service.LoaiKhoanChi().ToList();
            ViewBag.listkhoanchi = list;
            return PartialView("KhoanChiCDForm");
        }

        [HttpPost]
        public ActionResult Delete(int itemId, int ma)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = (NguoiDung)Session["User"];
            bool x;
            switch (ma)
            {
                case 0: x = service.XoaKCCD(itemId, user.mand); break;
                case 1: x = service.XoaKTCD(itemId, user.mand); break;
                case 2: x = service.XoaKC(itemId, user.mand); break;
                case 3: x = service.XoaKT(itemId, user.mand); break;
            }

            return RedirectToAction("ChiTieu");
        }

        [HttpPost]
        public ActionResult CreateUpdateKCCD(CTKhoanChiCD kccd)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = (NguoiDung)Session["User"];
            List<LoaiKhoanChi> list = service.LoaiKhoanChi().ToList();
            int ma = list.Single(s => s.tenloaichi == kccd.tenloaichi).maloaichi;
            kccd.maloaichi = ma;
            kccd.mand = user.mand;
            if (ModelState.IsValid)
            {
                if (kccd.makhoanchi > 0)
                {
                    service.UpdateKCCD(kccd);
                }
                else
                {
                    service.AddKCCD(kccd);
                }
            }
            return RedirectToAction("ChiTieu");
        }
    }
}
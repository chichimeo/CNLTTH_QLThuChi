using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMVC.SoChiTieu;

namespace DemoMVC.Controllers
{
    public class SoChiTieuController : Controller
    {

        private ChiChiService service = new ChiChiService();
        // GET: SoChiTieu
        public ActionResult ChiTieu(string SearchString)
        {
            DateTime now = DateTime.Now;
            string mathang = now.Year.ToString() + "-" + now.Month.ToString();
            //string mathang = "2020-10";
            List<CTKhoanChiCD> khoanchicd = service.KhoanChiCoDinh(mathang).ToList();
            List<CTKhoanChi> khoanchi = service.KhoanChiTheoThang(mathang).ToList();
            List<CTKhoanThu> khoanthu = service.KhoanThuTheoThang(mathang).ToList();
            List<CTKHoanThuCD> khoanthucd = service.KhoanThuCoDinh(mathang).ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                 khoanchicd = service.SearchKhoanChiCoDinh(mathang,SearchString).ToList();
                 khoanchi = service.SearchKhoanChiTheoThang(mathang, SearchString).ToList();
                 khoanthu = service.SearchKhoanThuTheoThang(mathang, SearchString).ToList();
                 khoanthucd = service.SearchKhoanThuCoDinh(mathang, SearchString).ToList();
            }
            ViewBag.khoanchicd = khoanchicd;
            ViewBag.khoanchi = khoanchi;
            ViewBag.khoanthucd = khoanthucd;
            ViewBag.khoanthu = khoanthu;
            return View();
        }

        public ActionResult AddEditKhoanChiCD(int itemId)
        {
            CTKhoanChiCD model = new CTKhoanChiCD();
            if (itemId > 0)
            {
                model = service.kccd(itemId);
            }
            ViewBag.khoanchi = model;
            List<LoaiKhoanChi> list = service.LoaiKhoanChi().ToList();
            ViewBag.listkhoanchi = list;
            return PartialView("KhoanChiCDForm");
        }

        [HttpPost]
        public ActionResult Delete(int itemId, int ma)
        {
            bool x;
            switch (ma)
            {
                case 0: x = service.XoaKCCD(itemId); break;
                case 1: x = service.XoaKTCD(itemId); break;
                case 2: x = service.XoaKC(itemId); break;
                case 3: x = service.XoaKT(itemId); break;
            }

            return RedirectToAction("ChiTieu");
        }

        [HttpPost]
        public ActionResult CreateUpdateKCCD(CTKhoanChiCD kccd)
        {
            List<LoaiKhoanChi> list = service.LoaiKhoanChi().ToList();
            int ma = list.Single(s => s.tenloaichi == kccd.tenloaichi).maloaichi;
            kccd.maloaichi = ma;
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
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
        public ActionResult ChiTieu()
        {
            DateTime now = DateTime.Now;
            string mathang = now.Year.ToString() + "-" + now.Month.ToString();
            List<KhoanChiCD> khoanchicd=  service.KhoanChiCoDinh(mathang).ToList();
            List<KhoanChi> khoanchi = service.KhoanChiTheoThang(mathang).ToList();
            List<KhoanThu> khoanthu = service.KhoanThuTheoThang(mathang).ToList();
            List<KhoanThuCD> khoanthucd = service.KhoanThuCoDinh(mathang).ToList();
            ViewBag.khoanchicd = khoanchicd;
            ViewBag.khoanchi = khoanchi;
            ViewBag.khoanthucd = khoanthu;
            ViewBag.khoanthu = khoanthu;
            return View();
        }
    }
}
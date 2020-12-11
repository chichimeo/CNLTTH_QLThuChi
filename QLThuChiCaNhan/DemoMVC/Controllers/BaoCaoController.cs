using DemoMVC.DangNhap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoMVC.Controllers
{
    public class BaoCaoController : Controller
    {
        private TuTuService service = new TuTuService();
        // GET: BaoCao
        public ActionResult BaoCao()
        {
            return View();
        }


        public ActionResult Report()
        {
            if (Session["User"] != null)
            {
                NguoiDung nd = (NguoiDung)Session["User"];
                int id = nd.mand;
                int thang = DateTime.Now.Month;
                int nam = DateTime.Now.Year;
                int tongchi = service.tinhTongChi(id, thang, nam);
                int tongthu = service.tinhTongThu(id, thang, nam);

                List<TongChiTheoLoai> chitheoloai = service.DSChiTheoLoai(id, thang, nam).ToList();
                List<TongThuTheoLoai> thutheoloai = service.DSThuTheoLoai(id, thang, nam).ToList();

                int k = chitheoloai.Count;
                int h = thutheoloai.Count;

                int i, j;

                string[] loaichi = new string[k];
                int[] tienchi = new int[k];
                string[] loaithu = new string[h];
                int[] tienthu = new int[h];

                for (i = 0; i < k; i++)
                {
                    loaichi[i] = chitheoloai[i].tenloaichi;
                    tienchi[i] = int.Parse(chitheoloai[i].TongChi.ToString());
                }

                for (j = 0; j < h; j++)
                {
                    loaithu[j] = thutheoloai[j].tenloaithu;
                    tienchi[j] = int.Parse(thutheoloai[j].TongThu.ToString());
                }

                ViewBag.chitheoloai = chitheoloai;
                ViewBag.thutheoloai = thutheoloai;

                ViewBag.loaichi = loaichi;
                ViewBag.tienchi = tienchi;
                ViewBag.loaithu = loaithu;
                ViewBag.tienthu = tienthu;
                ViewBag.lengthchi = k;
                ViewBag.lengththu = h;

                ViewBag.tongchi = tongchi;
                ViewBag.tongthu = tongthu;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
    }
}
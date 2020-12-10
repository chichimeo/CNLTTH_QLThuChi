using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Service
{
    /// <summary>
    /// Summary description for ChiChiService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ChiChiService : System.Web.Services.WebService
    {
        private DataClassesDataContext context = new DataClassesDataContext();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(Description = "Khoan thu theo thang")]
        public List<CTKhoanThu> KhoanThuTheoThang(string MaThang)
        {
            return context.CTKhoanThus.Where(s => s.ngaythu.ToString().Contains(MaThang)).ToList();
        }

        [WebMethod(Description = "Khoan thu theo thang")]
        public List<CTKhoanThu> SearchKhoanThuTheoThang(string MaThang, string stringsearch)
        {
            return context.CTKhoanThus.Where(s => s.ngaythu.ToString().Contains(MaThang)
            && (s.tenkhoanthu.Contains(stringsearch))
            ).ToList() 
                ;
        }

        [WebMethod(Description = "Khoan chi theo thang")]
        public List<CTKhoanChi> KhoanChiTheoThang(string MaThang)
        {
            return context.CTKhoanChis.Where(s => s.ngaychi.ToString().Contains(MaThang)).ToList();
        }

        [WebMethod(Description = "Khoan chi theo thang")]
        public List<CTKhoanChi> SearchKhoanChiTheoThang(string MaThang, string stringsearch)
        {
            return context.CTKhoanChis.Where(s => s.ngaychi.ToString().Contains(MaThang)
            && (s.tenkhoanchi.Contains(stringsearch))
            ).ToList();
        }

        [WebMethod(Description = "Khoan thu co dinh")]
        public List<CTKHoanThuCD> KhoanThuCoDinh(string MaThang)
        {
            int mt = int.Parse(MaThang.Substring(0, 4) + MaThang.Substring(5, 2));
            List<CTKHoanThuCD> KhoanThuCD = context.CTKHoanThuCDs.ToList();
            for (int i = KhoanThuCD.Count - 1; i >= 0; i--)
            {
                int ngaybd = int.Parse(KhoanThuCD[i].thangbd.Value.Year.ToString() + KhoanThuCD[i].thangbd.Value.Month.ToString());
                int ngaykt = int.Parse(KhoanThuCD[i].thangkt.Value.Year.ToString() + KhoanThuCD[i].thangkt.Value.Month.ToString());


                if (!(ngaybd <= mt && ngaykt >= mt))
                {
                    KhoanThuCD.RemoveAt(i);
                }
            }
            return KhoanThuCD;
        }

        [WebMethod(Description = "Khoan thu co dinh")]
        public List<CTKHoanThuCD> SearchKhoanThuCoDinh(string MaThang, string searchstring)
        {
            int mt = int.Parse(MaThang.Substring(0, 4) + MaThang.Substring(5, 2));
            List<CTKHoanThuCD> KhoanThuCD = context.CTKHoanThuCDs.Where(s=> 
            s.tenkhoanthucd.Contains(searchstring)).ToList();
            for (int i = KhoanThuCD.Count - 1; i >= 0; i--)
            {
                int ngaybd = int.Parse(KhoanThuCD[i].thangbd.Value.Year.ToString() + KhoanThuCD[i].thangbd.Value.Month.ToString());
                int ngaykt = int.Parse(KhoanThuCD[i].thangkt.Value.Year.ToString() + KhoanThuCD[i].thangkt.Value.Month.ToString());


                if (!(ngaybd <= mt && ngaykt >= mt))
                {
                    KhoanThuCD.RemoveAt(i);
                }
            }
            return KhoanThuCD;
        }

        [WebMethod(Description = "Khoan chi co dinh")]
        public List<CTKhoanChiCD> KhoanChiCoDinh(string MaThang)
        {
            int mt = int.Parse(MaThang.Substring(0, 4) + MaThang.Substring(5, 2));
            List<CTKhoanChiCD> kccd = context.CTKhoanChiCDs.ToList();
            for (int i = kccd.Count - 1; i >= 0; i--)
            {
                int ngaybd = int.Parse(kccd[i].thangbd.Value.Year.ToString() + kccd[i].thangbd.Value.Month.ToString());
                int ngaykt = int.Parse(kccd[i].thangkt.Value.Year.ToString() + kccd[i].thangkt.Value.Month.ToString());

                
                if (!(ngaybd <= mt && ngaykt >= mt))
                {
                    kccd.RemoveAt(i);
                }
            }
            return kccd;
        }

        [WebMethod(Description = "Khoan chi co dinh")]
        public List<CTKhoanChiCD> SearchKhoanChiCoDinh(string MaThang, string stringseach)
        {
            int mt = int.Parse(MaThang.Substring(0, 4) + MaThang.Substring(5, 2));
            List<CTKhoanChiCD> kccd = context.CTKhoanChiCDs.Where(s =>
            s.tenkhoanchi.Contains(stringseach)).ToList();
            for (int i = kccd.Count - 1; i >= 0; i--)
            {
                int ngaybd = int.Parse(kccd[i].thangbd.Value.Year.ToString() + kccd[i].thangbd.Value.Month.ToString());
                int ngaykt = int.Parse(kccd[i].thangkt.Value.Year.ToString() + kccd[i].thangkt.Value.Month.ToString());


                if (!(ngaybd <= mt && ngaykt >= mt))
                {
                    kccd.RemoveAt(i);
                }
            }
            return kccd;
        }

        [WebMethod(Description = "Loai khoan chi")]
        public List<LoaiKhoanChi> LoaiKhoanChi()
        {
            return context.LoaiKhoanChis.ToList();
        }

        [WebMethod(Description = "Loai khoan thu")]
        public List<LoaiKhoanThu> LoaiKhoanThu()
        {
            return context.LoaiKhoanThus.ToList();
        }

        [WebMethod(Description = "Tim mot khoan thu co dinh")]
        public CTKHoanThuCD ktcd(int ma)
        {
            return context.CTKHoanThuCDs.Single(s => s.makhoanthucd == ma);
        }

        [WebMethod(Description = "Tim mot khoan chi co dinh")]
        public CTKhoanChiCD kccd(int ma)
        {
            return context.CTKhoanChiCDs.Single(s => s.makhoanchi == ma);
        }

        [WebMethod(Description = "Xoa mot khoan thu co dinh")]
        public bool XoaKTCD(int ma)
        {
            try
            {
                KhoanThuCD kt = context.KhoanThuCDs.Single(sv => sv.makhoanthucd == ma);
                kt.thangkt = DateTime.Now.AddMonths(-1);
                context.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [WebMethod(Description = "Xoa mot khoan thu ")]
        public bool XoaKT(int ma)
        {
            try
            {
                KhoanThu kt = context.KhoanThus.Single(sv => sv.makhoanthu == ma);
                context.KhoanThus.DeleteOnSubmit(kt);
                context.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [WebMethod(Description = "Xoa mot khoan chi co dinh")]
        public bool XoaKCCD(int ma)
        {
            try
            {
                KhoanChiCD kt = context.KhoanChiCDs.Single(sv => sv.makhoanchi == ma);
                kt.thangkt = DateTime.Now.AddMonths(-1);
                context.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [WebMethod(Description = "Xoa mot khoan chi")]
        public bool XoaKC(int ma)
        {
            try
            {
                KhoanChi kt = context.KhoanChis.Single(sv => sv.makhoanchi == ma);
                context.KhoanChis.DeleteOnSubmit(kt);
                context.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [WebMethod(Description = "Xoa mot khoan chi")]
        public bool kiemtra(DateTime s)
        {
            int x= int.Parse(s.Year.ToString() + s.Month.ToString());
            int y = 202012;
           
            return (x >= y);
        }
        [WebMethod(Description = "Update mot khoan chi")]
        public bool UpdateKCCD (CTKhoanChiCD news)
        {

            try
            {
                KhoanChiCD kt = context.KhoanChiCDs.Single(sv => sv.makhoanchi == news.makhoanchi);
                kt.tenkhoanchi = news.tenkhoanchi;
                kt.tienchicd = news.tienchicd;
                kt.thangbd = news.thangbd;
                kt.thangkt = news.thangkt;
                kt.maloaichi = news.maloaichi;
                context.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [WebMethod(Description = "Them mot khoan chi")]
        public bool AddKCCD(CTKhoanChiCD news)
        {

            try
            {
                KhoanChiCD kt = new KhoanChiCD();
                kt.tenkhoanchi = news.tenkhoanchi;
                kt.tienchicd = news.tienchicd;
                kt.thangbd = news.thangbd;
                kt.thangkt = news.thangkt;
                kt.maloaichi = news.maloaichi;
                context.KhoanChiCDs.InsertOnSubmit(kt);
                context.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

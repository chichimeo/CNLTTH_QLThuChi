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
        public List<KhoanThu> KhoanThuTheoThang (string MaThang)
        {
            return context.KhoanThus.Where(s => s.ngaythu.ToString().Contains(MaThang)).ToList();
        }

        [WebMethod(Description = "Khoan chi theo thang")]
        public List<KhoanChi> KhoanChiTheoThang(string MaThang)
        {
            return context.KhoanChis.Where(s => s.ngaychi.ToString().Contains(MaThang)).ToList();
        }

        [WebMethod(Description = "Khoan thu co dinh")]
        public List<KhoanThuCD> KhoanThuCoDinh(string MaThang)
        {
            int mt = int.Parse(MaThang.Substring(0, 4) + MaThang.Substring(5, 2));
            List<KhoanThuCD> KhoanThuCD = context.KhoanThuCDs.ToList();
            foreach (KhoanThuCD x in KhoanThuCD)
            {
                int ngaybd = int.Parse(x.thangbd.Value.Year.ToString() + x.thangbd.Value.Month.ToString());
                int ngaykt = int.Parse(x.thangkt.Value.Year.ToString() + x.thangkt.Value.Month.ToString());

                if (!(ngaybd <= mt && ngaykt >= mt))
                {
                    KhoanThuCD.Remove(x);
                }
            }
            return KhoanThuCD;
        }

        [WebMethod(Description = "Khoan chi co dinh")]
        public List<KhoanChiCD> KhoanChiCoDinh(string MaThang)
        {
            int mt = int.Parse(MaThang.Substring(0, 4) + MaThang.Substring(5, 2));
            List<KhoanChiCD> kccd = context.KhoanChiCDs.ToList();
            foreach (KhoanChiCD x in kccd)
            {
                int ngaybd = int.Parse(x.thangbd.Value.Year.ToString() + x.thangbd.Value.Month.ToString());
                int ngaykt = int.Parse(x.thangkt.Value.Year.ToString() + x.thangkt.Value.Month.ToString());

                if (!(ngaybd <= mt && ngaykt >= mt))
                {
                    kccd.Remove(x);
                }
            }
            return kccd;
        }

    }
}

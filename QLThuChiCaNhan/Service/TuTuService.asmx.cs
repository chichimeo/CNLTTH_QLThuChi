using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

namespace Service
{
    /// <summary>
    /// Summary description for TuTuService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TuTuService : System.Web.Services.WebService
    {
        private DataClassesDataContext context = new DataClassesDataContext();

        [WebMethod(Description = "Kiem tra dang nhap")]
        public NguoiDung KiemTraDN(string username, string password)
        {
            return context.NguoiDungs.Single(u => u.tendn.Equals(username) && u.matkhau.Equals(password));
        }

        [WebMethod(Description = "Them nguoi dung dang ky moi")]
        public bool ktraThemND(string email, string username)
        {
            try
            {
                var user = context.NguoiDungs.Where(x => x.email.Equals(email) && x.tendn.Equals(username)).ToList();
                if (user != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false; 
            }
        }

        [WebMethod(Description ="Them nguoi dung dang ky moi")]
        public bool ThemNguoiDung(string name, string email, string username, string password)
        {
            try
            {
                if (IsOnlyWord(name) && IsValidEmail(email) && password.Length >= 3){
                    NguoiDung nd = new NguoiDung();
                    nd.tennd = name;
                    nd.email = email;
                    nd.tendn = username;
                    nd.matkhau = password;
                    context.NguoiDungs.InsertOnSubmit(nd);
                    context.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }            
            }
            catch
            {
                return false;
            }
        }

        [WebMethod(Description = "Tinh tong chi trong thang tuong ung cua nguoi dung")]
        public int tinhTongChi(int id, int thang, int nam)
        {
            var u = context.TongChiNDs.Single(x => x.mand.Equals(id) && x.thangchi == thang && x.namchi == nam);
            int tongchi = int.Parse(u.TongChi.ToString());
            return tongchi;
        }

        [WebMethod(Description = "Tinh tong thu trong thang tuong ung cua nguoi dung")]
        public int tinhTongThu(int id, int thang, int nam)
        {
            var u = context.TongThuNDs.Single(x => x.mand.Equals(id) && x.thangthu == thang && x.namthu == nam);
            int tongthu = int.Parse(u.TongThu.ToString());
            return tongthu;
        }

        [WebMethod(Description = "Tinh tong chi theo loai trong thang tuong ung cua nguoi dung")]
        public int tinhTongChiTheoLoai(int id, int thang, int nam, int maloai)
        {
            var u = context.TongChiTheoLoais.Single(x => x.mand.Equals(id) && x.thangchi == thang && x.namchi == nam && x.maloaichi.Equals(maloai));
            int d = int.Parse(u.TongChi.ToString());
            return d;
        }

        [WebMethod(Description = "Tinh tong thu theo loai trong thang tuong ung cua nguoi dung")]
        public int tinhTongThuTheoLoai(int id, int thang, int nam, int maloai)
        {
            var u = context.TongThuTheoLoais.Single(x => x.mand.Equals(id) && x.thangthu == thang && x.namthu == nam && x.maloaithu.Equals(maloai));
            int d = int.Parse(u.TongThu.ToString());
            return d;
        }

        [WebMethod(Description = "Liet ke tong chi theo loai")]
        public List<TongChiTheoLoai> DSChiTheoLoai(int id, int thang, int nam)
        {
            List<TongChiTheoLoai> list = context.TongChiTheoLoais.Where(x => x.mand.Equals(id) && x.thangchi == thang && x.namchi == nam).ToList();
            return list;
        }

        [WebMethod(Description = "Liet ke tong thu theo loai")]
        public List<TongThuTheoLoai> DSThuTheoLoai(int id, int thang, int nam)
        {
            List<TongThuTheoLoai> list = context.TongThuTheoLoais.Where(x => x.mand.Equals(id) && x.thangthu == thang && x.namthu == nam).ToList();
            return list;
        }


        //xử lý khi đăng ký
        public bool IsOnlyWord(string pText)
        {
            Regex regex = new Regex(@"[A-Za-z]");
            return regex.IsMatch(pText);
        }

        public bool IsValidEmail(string ptext)
        {
            Regex regex = new Regex(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}");
            return regex.IsMatch(ptext);
        }
    }
}

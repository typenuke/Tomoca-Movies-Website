using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebXemPhim.Models;

namespace WebXemPhim.Areas.Admin.Controllers
{
    public class TaiKhoanController : BaseController
    {
        //
        // GET: /Admin/TaiKhoan/

        DataClasses1DataContext data = new DataClasses1DataContext();
        public ActionResult DSTaiKhoan()
        {
            var tk = data.TaiKhoans.ToList();
            return View(tk);
        }

        //Xóa TK
        public ActionResult XoaTK(string tendn)
        {
            TaiKhoan tk = data.TaiKhoans.SingleOrDefault(n => n.TenDN == tendn);
            if (tk == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            data.TaiKhoans.DeleteOnSubmit(tk);
            data.SubmitChanges();
            return RedirectToAction("DSTaiKhoan");
        }
        //Thêm tài khoản
        [HttpGet]
        public ActionResult ThemTK()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemTK(TaiKhoan tk ,FormCollection coll)
        {
            var tendn = coll["TenDN"];
            var mk = coll["MatKhau"];   
            //var taikhoan = from t in data.TaiKhoans where t.TenDN.Equals(tendn) select t.TenDN;
            var taikhoan = data.TaiKhoans.ToList();
            int kt=0;
            foreach (var item in taikhoan)
            {
                if (item.TenDN == tendn)
                    kt = 1;
            }
            if(String.IsNullOrEmpty(tendn))
                ViewData["Loi"]="Tên đăng nhập không được để chống";
            else if (String.IsNullOrEmpty(mk))
                ViewData["Loi1"] = "Mật khẩu không được để chống";
            else if (kt==1)
            {
                ViewData["Loi2"] = "Đã có tài khoản này";
            }
            else
            {
                tk.TenDN = tendn;
                tk.MatKhau = mk;
                data.TaiKhoans.InsertOnSubmit(tk);
                data.SubmitChanges();
                return RedirectToAction("DSTaiKhoan");
            }
            return View();
        }
        //Sửa
        public ActionResult SuaTK(string tendn)
        {
            var tk = data.TaiKhoans.First(n => n.TenDN == tendn);
            if (tk == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(tk);
        }

        [HttpPost]
        public ActionResult SuaTK(string tendn, FormCollection collection)
        {
            var tk = data.TaiKhoans.First(n => n.TenDN == tendn);
            var matkhau = collection["MatKhau"];
            var img = collection["Img"];
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi"] = "Không được để trống";
            }
            else
            {
                tk.MatKhau = matkhau;
                UpdateModel(tk);
                data.SubmitChanges();
                return RedirectToAction("DSTaiKhoan");
            }
            return this.SuaTK(tendn);
        }

    }
}

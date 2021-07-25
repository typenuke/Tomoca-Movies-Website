using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TomocaMoviesWebsite.Models;

namespace TomocaMoviesWebsite.Controllers
{
    public class adminmanagerController : BaseController
    {
        // GET: adminmanager
        TomocaMoviesDataContext data = new TomocaMoviesDataContext();
        public ActionResult users()
        {
            var tk = data.Users.ToList();
            return View(tk);
        }
        public ActionResult delete(int id)
        {
            User tk = data.Users.SingleOrDefault(n => n.UserID == id);
            if (tk == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            data.Users.DeleteOnSubmit(tk);
            data.SubmitChanges();
            return RedirectToAction("users");
        }
        //Thêm tài khoản
        public ActionResult add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult add(User tk, FormCollection coll)
        {
            var tendn = coll["TenDN"];
            var mk = coll["MatKhau"];
            var mknhaplai = coll["MatKhauNhapLai"];
            var fn = coll["TenDau"];
            var ln = coll["TenCuoi"];
            var em = coll["email"];
            var ph = coll["sodienthoai"];
            //var taikhoan = from t in data.TaiKhoans where t.TenDN.Equals(tendn) select t.TenDN;
            var taikhoan = data.Users.ToList();
            int kt = 0;
            int kt2 = 0;
            var ktem = data.Users.Where(a => a.Email == em);
            foreach (var item in taikhoan)
            {
                if (item.Username == tendn)
                    kt = 1;
                if (item.Phone == ph)
                    kt2 = 1;
            }
            if (String.IsNullOrEmpty(tendn))
                ViewData["Loi"] = "Tên đăng nhập không được để chống";
            else if (String.IsNullOrEmpty(mk))
                ViewData["Loi1"] = "Mật khẩu không được để chống";
            else if (String.IsNullOrEmpty(em))
                ViewData["Loi2"] = "Vui lòng nhập Email";
            else if (String.IsNullOrEmpty(ph))
                ViewData["Loi3"] = "Vui lòng nhập số điện thoại";
            else if (kt == 1)
            {
                ViewData["Loi2"] = "Đã có tài khoản này";
            }
            else if (ktem.Count() != 0)
            {
                ViewData["Loi2e"] = "Email đã được sử dụng";
            }
            else if (kt2 == 1)
            {
                ViewData["Loi3e"] = "Số điện thoại đã được sử dụng";
            }
            else if (mk != mknhaplai)
            {
                ViewData["Loi12"] = "Mật khẩu nhập lại không đúng";
            }
            else
            {
                tk.Username = tendn;
                tk.Password = mk;
                tk.FirstName = fn;
                tk.LastName = ln;
                tk.Email = em;
                tk.Phone = ph;
                data.Users.InsertOnSubmit(tk);
                data.SubmitChanges();
                return RedirectToAction("users");
            }
            return View();
        }
        //Sửa
        public ActionResult edit(string id)
        {
            var tk = data.Users.First(n => n.UserID.ToString() == id);
            if (tk == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(tk);
        }

        [HttpPost]
        public ActionResult edit(string id, FormCollection collection)
        {
            var tk = data.Users.First(n => n.UserID.ToString() == id);
            var taikhoan = data.Users.ToList();
            var fn = collection["FirstName"];
            var ln = collection["LastName"];
            var em = collection["Email"];
            var ph = collection["Phone"];
            var matkhau = collection["Password"];
            int kt2 = 0;
            var ktem = data.Users.Where(a => a.Email == em && tk.Email != em);
            var ktph = data.Users.Where(a => a.Phone == em && tk.Phone != em);
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi"] = "Không được để trống";
            }
            else if (String.IsNullOrEmpty(em))
                ViewData["Loi2"] = "Vui lòng nhập Email";
            else if (String.IsNullOrEmpty(ph))
                ViewData["Loi3"] = "Vui lòng nhập số điện thoại";
            else if (ktem.Count() != 0)
            {
                ViewData["Loi2e"] = "Email đã được sử dụng";
            }
            else if (ktph.Count() != 0)
            {
                ViewData["Loi3e"] = "Số điện thoại đã được sử dụng";
            }
            else
            {
                tk.Password = matkhau;
                tk.Phone = ph;
                tk.Email = em;
                tk.FirstName = fn;
                tk.LastName = ln;
                UpdateModel(tk);
                data.SubmitChanges();
                return RedirectToAction("users");
            }
            return this.edit(id);
        }

    }
}
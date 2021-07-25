using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TomocaMoviesWebsite.Models;

namespace TomocaMoviesWebsite.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        TomocaMoviesDataContext db = new TomocaMoviesDataContext();

        public ActionResult Login(FormCollection collection)
        {
            var un = collection["UserName"];
            var pw = collection["Password"];

            
            if (String.IsNullOrEmpty(un))
                ViewData["Loi"] = "Bạn phải nhập tên đăng nhập";
            else if (String.IsNullOrEmpty(pw))
            {
                ViewData["Loi1"] = "Bạn phải nhập mật khẩu";
            }
            else
            {
                User tk = db.Users.SingleOrDefault(n => n.Username.Equals(un) && n.Password.Equals(pw));
                if (tk != null)
                {
                    if (tk.Permission == true)//Admin
                    {
                        @Session["Username"] = tk.Username;
                        @Session["Permission"] = 1;
                        ViewBag.ThongBao = "Đăng nhập thành công admin";
                        return RedirectToAction("Index", "Tomoca");
                    }
                    if (tk.Permission == false || tk.Permission == null)
                    {
                        @Session["Username"] = tk.Username;
                        @Session["Permission"] = null;
                        ViewBag.ThongBao = "Đăng nhập thành công";
                        return RedirectToAction("Index", "Tomoca");
                    }
                }
                else
                {
                    ViewData["Loi2"] = "Tên đăng nhâp hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User tk, FormCollection coll)
        {
            var tendn = coll["TenDN"];
            var mk = coll["MatKhau"];
            var mknhaplai = coll["MatKhauNhapLai"];
            var fn = coll["TenDau"];
            var ln = coll["TenCuoi"];
            var em = coll["email"];
            var ph = coll["sodienthoai"];
            //var taikhoan = from t in data.TaiKhoans where t.TenDN.Equals(tendn) select t.TenDN;
            var taikhoan = db.Users.ToList();
            int kt = 0;
            foreach (var item in taikhoan)
            {
                if (item.Username == tendn)
                    kt = 1;
            }
            if (String.IsNullOrEmpty(tendn))
                ViewData["Loi"] = "Tên đăng nhập không được để chống";
            else if (String.IsNullOrEmpty(mk))
                ViewData["Loi1"] = "Mật khẩu không được để chống";
            else if (kt == 1)
            {
                ViewData["Loi2"] = "Đã có tài khoản này";
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
                db.Users.InsertOnSubmit(tk);
                db.SubmitChanges();
                return RedirectToAction("signupfinish");
            }
            return View();
        }
        public ActionResult signupfinish()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Tomoca");
        }
        public ActionResult profile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult changeprofile(string username)
        {
            var tk = db.Users.First(n => n.Username == username);
            if (tk == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(tk);
        }

        [HttpPost]
        public ActionResult changeprofile(string username, FormCollection collection)
        {
            var tk = db.Users.First(n => n.Username == username);
            var taikhoan = db.Users.ToList();
            var fn = collection["FirstName"];
            var ln = collection["LastName"];
            var em = collection["Email"];
            var ph = collection["Phone"];
            var matkhau = collection["Password"];
            int kt2 = 0;
            var ktem = db.Users.Where(a => a.Email == em && tk.Email != em);
            var ktph = db.Users.Where(a => a.Phone == em && tk.Phone != em);
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
                db.SubmitChanges();
                return RedirectToAction("users");
            }
            return this.changeprofile(username);
        }
    }
}
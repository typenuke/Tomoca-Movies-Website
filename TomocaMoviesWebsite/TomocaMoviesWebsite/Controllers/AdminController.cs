using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TomocaMoviesWebsite.Models;

namespace TomocaMoviesWebsite.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Amin
        TomocaMoviesDataContext data = new TomocaMoviesDataContext();
        public ActionResult Home()
        {
            int demnd = data.Users.ToList().Count;
            var doanhthu = 0.0;
            var tk = data.Tickets.OrderByDescending(x => x.TicketID).ToList();
            foreach(var i in tk)
            {
                doanhthu += i.Money;
            }
            ViewData["doanhthu"] = doanhthu;
            int phimsapchieu = data.Movies.Where(x => x.ComingSoon == 1).ToList().Count;
            int phimdangchieu = data.Movies.Where(x => x.ComingSoon == 0).ToList().Count;
            ViewData["NguoiDung"] = demnd;
            ViewData["phimsapchieu"] = phimsapchieu;
            ViewData["phimdangchieu"] = phimdangchieu;
            var DSPhimSapChieu = data.Movies.Where(x => x.ComingSoon == 1).Take(10).ToList();
            ViewData["DSPhimSapChieu"] = DSPhimSapChieu;
            var DSPhimDangChieu = data.Movies.OrderByDescending(x => x.MovieID).Take(10).ToList();
            ViewData["DSPhimDangChieu"] = DSPhimDangChieu;
            var DSDatVe = data.Tickets.OrderByDescending(a => a.TicketID).Take(10).ToList();
            ViewData["DSDatVe"] = DSDatVe;
            return View();
        }
        //Actor thêm, xóa, sửa
        public ActionResult DienVien()
        {
            var ac = data.Actors.ToList();
            return View(ac);
        }
        [HttpGet]
        public ActionResult ThemDienVien()
        {
            return View();
        }
        //Thêm thể loai
        [HttpPost]
        public ActionResult ThemDienVien(Actor ac, FormCollection coll)
        {

            var ten = coll["Name"];
            var noisinh = coll["Nationality"];
            var ngaysinh = coll["Birth"];
            if (String.IsNullOrEmpty(ten))
                ViewData["Loi1"] = "Tên thể diễn viên được để chống";
            if (String.IsNullOrEmpty(noisinh))
                ViewData["Loi1"] = "Tên thể diễn viên được để chống";
            if (String.IsNullOrEmpty(ngaysinh))
                ViewData["Loi1"] = "Tên thể diễn viên được để chống";
            else
            {
                ac.Name = ten;
                ac.Nationality = noisinh;
                ac.Birth = DateTime.Parse(ngaysinh);
                data.Actors.InsertOnSubmit(ac);
                data.SubmitChanges();
                return RedirectToAction("DienVien");
            }
            return View();
        }
        //Xóa thể loai
        public ActionResult XoaDienVien(int id)
        {
            Actor dg = data.Actors.SingleOrDefault(n => n.ActorID == id);
            if (dg == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            data.Actors.DeleteOnSubmit(dg);
            data.SubmitChanges();
            return RedirectToAction("DienVien");
        }
        //Sửa thể loại
        public ActionResult SuaDienVien(int id)
        {
            var tl = data.Actors.First(n => n.ActorID == id);
            if (tl == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(tl);
        }
        [HttpPost]
        public ActionResult SuaDienVien(int id, FormCollection collection)
        {
            var ac = data.Actors.First(n => n.ActorID == id);
            var ten = collection["Name"];
            var noisinh = collection["Nationality"];
            var ngaysinh = collection["Birth"];
            var theloai = data.Directors.ToList();
            ac.Name = ten;
            ac.Nationality = noisinh;
            ac.Birth = DateTime.Parse(ngaysinh);
            UpdateModel(ac);
            data.SubmitChanges();
            return RedirectToAction("DienVien");
        }
        //Thêm Sửa Xóa Đạo diễn
        public ActionResult DaoDien()
        {
            var ac = data.Directors.ToList();
            return View(ac);
        }
        [HttpGet]
        public ActionResult ThemDaoDien()
        {
            return View();
        }
        //Thêm thể loai
        [HttpPost]
        public ActionResult ThemDaoDien(Director ac, FormCollection coll)
        {
            var ten = coll["Name"];
            var noisinh = coll["Nationality"];
            var ngaysinh = coll["Birth"];
            if (String.IsNullOrEmpty(ten))
                ViewData["Loi1"] = "Tên thể diễn viên được để chống";
            if (String.IsNullOrEmpty(noisinh))
                ViewData["Loi1"] = "Tên thể diễn viên được để chống";
            if (String.IsNullOrEmpty(ngaysinh))
                ViewData["Loi1"] = "Tên thể diễn viên được để chống";
            else
            {
                ac.Name = ten;
                ac.Nationality = noisinh;
                ac.Birth = DateTime.Parse(ngaysinh);
                data.Directors.InsertOnSubmit(ac);
                data.SubmitChanges();
                return RedirectToAction("DaoDien");
            }
            return View();
        }
        //Xóa thể loai
        public ActionResult XoaDaoDien(int id)
        {
            Director tl = data.Directors.SingleOrDefault(n => n.DirectorID == id);
            if (tl == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            data.Directors.DeleteOnSubmit(tl);
            data.SubmitChanges();
            return RedirectToAction("DaoDien");
        }
        //Sửa thể loại
        public ActionResult SuaDaoDien(int id)
        {
            var tl = data.Directors.First(n => n.DirectorID == id);
            if (tl == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(tl);
        }
        [HttpPost]
        public ActionResult SuaDaoDien(int id, FormCollection collection)
        {
            var ac = data.Directors.First(n => n.DirectorID == id);
            var ten = collection["Name"];
            var noisinh = collection["Nationality"];
            var ngaysinh = collection["Birth"];
            var theloai = data.Directors.ToList();
            ac.Name = ten;
            ac.Nationality = noisinh;
            ac.Birth = DateTime.Parse(ngaysinh);
            UpdateModel(ac);
            data.SubmitChanges();
            return RedirectToAction("DaoDien");
        }
        //Thể loại Thêm Xóa Sửa
        public ActionResult TheLoai()
        {
            var tl = data.Genres.ToList();
            return View(tl);
        }
        [HttpGet]
        public ActionResult ThemTheLoai()
        {
            return View();
        }
        //Thêm thể loai
        [HttpPost]
        public ActionResult ThemTheLoai(Genre tl, FormCollection coll)
        {

            var ten = coll["GenreName"];
            var theloai = data.Genres.ToList();

            if (String.IsNullOrEmpty(ten))
                ViewData["Loi1"] = "Tên thể loại không được để chống";

            else
            {
                foreach (var item in theloai)
                {
                    if (String.Compare(item.GenreName, ten, true) == 0)
                    {
                        ViewData["Loi2"] = "Đã có thể loại này";
                        return View();
                    }
                }
                tl.GenreName = ten;
                data.Genres.InsertOnSubmit(tl);
                data.SubmitChanges();
                return RedirectToAction("TheLoai");
            }
            return View();
        }
        //Xóa thể loai
        public ActionResult XoaTheLoai(int id)
        {
            Genre tl = data.Genres.SingleOrDefault(n => n.GenreID == id);
            if (tl == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            data.Genres.DeleteOnSubmit(tl);
            data.SubmitChanges();
            return RedirectToAction("TheLoai");
        }
        //Sửa thể loại
        public ActionResult SuaTheLoai(int id)
        {
            var tl = data.Genres.First(n => n.GenreID == id);
            if (tl == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(tl);
        }

        [HttpPost]
        public ActionResult SuaTheLoai(int id, FormCollection collection)
        {
            var tl = data.Genres.First(n => n.GenreID == id);
            var ten = collection["GenreName"];
            var theloai = data.Genres.ToList();
            foreach (var item in theloai)
            {
                if (String.Compare(item.GenreName, ten, true) == 0 && item.GenreID != id)
                {
                    ViewData["Loi2"] = "Tên thể loại đã tồn tại";
                    return this.SuaTheLoai(id);
                }
            }
            if (String.IsNullOrEmpty(ten))
            {
                ViewData["Loi1"] = "Tên thể loại không được để trống";
            }
            else
            {
                tl.GenreName = ten;
                UpdateModel(tl);
                data.SubmitChanges();
                return RedirectToAction("TheLoai");
            }
            return this.SuaTheLoai(id);
        }

        //News
        public ActionResult TinTuc()
        {
            var tt = data.News.ToList();
            return View(tt);
        }
        public ActionResult SuaTinTuc(int id)
        {
            New tin = data.News.SingleOrDefault(n => n.NewsID == id);
            if (tin == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(tin);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaTinTuc(int id, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            var tin = data.News.First(n => n.NewsID == id);
            var kt = data.News.ToList();
            if (fileUpload != null)
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/public/image"), fileName);
                foreach (var item in kt)
                {
                    if (String.Compare(item.Photo, fileName, true) == 0 && item.NewsID != id)
                    {
                        ViewBag.ThongBao = "Hình như cũ";
                        return this.SuaTinTuc(id);
                    }
                }
                fileUpload.SaveAs(path);
                tin.Photo = "~/public/image" + fileName;

            }
            UpdateModel(tin);
            data.SubmitChanges();
            return RedirectToAction("TinTuc");
        }
        public ActionResult XoaTinTuc(int id)
        {
            New tl = data.News.SingleOrDefault(n => n.NewsID == id);
            if (tl == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            data.News.DeleteOnSubmit(tl);
            data.SubmitChanges();
            return RedirectToAction("TinTuc");
        }

        public ActionResult ThemTinTuc()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemTinTuc(IEnumerable<HttpPostedFileBase> files, New tt)
        {
            User us = data.Users.First(ab => ab.Username == Session["Username"].ToString());
            DateTime a = DateTime.Now;
            var tintuc = data.News.ToList();
            int count = 1;
            foreach (var item in files)
            {
                if (item.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(item.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/image/news"), fileName);
                    item.SaveAs(path);
                    if (count == 1)
                    {

                        tt.Photo = "/Content/image/news/" + fileName;
                        
                    }
                    if (count == 2)
                    {

                        tt.Image1 = "/Content/image/news/" + fileName;
                    }
                    if (count == 3)
                    {

                        tt.Image2 = "/Content/image/news/" + fileName;
                    }
                    
                }
                if (count == item.ContentLength)
                    break;
                count++;
            }
            data.News.InsertOnSubmit(tt);
            data.SubmitChanges();
            return RedirectToAction("TinTuc");
        }
        public ActionResult veban()
        {
            var lst = from l in data.MiAnLiens select l;
            return View(lst);
        }
        public ActionResult ThemVePhim()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemVePhim(FormCollection coll, MiAnLien mal)
        {
            var movieid = data.Movies.First(x => String.Compare(x.Title, coll["tenphim"].ToString(), true) == 0);
            var theaterid = data.MovieTheaters.First(x => String.Compare(x.TheaterName, coll["theater"].ToString(), true) == 0);
            mal.Time = DateTime.Today.AddDays(7);
            mal.TheaterID = theaterid.TheaterID;
            mal.MovieID = movieid.MovieID;
            data.MiAnLiens.InsertOnSubmit(mal);
            data.SubmitChanges();
            return RedirectToAction("veban");
        }
    }
}
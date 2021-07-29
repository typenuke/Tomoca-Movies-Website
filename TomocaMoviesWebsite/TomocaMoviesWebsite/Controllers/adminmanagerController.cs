using System;
using System.Collections.Generic;
using System.IO;
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
        //phim đang chiếu
        public ActionResult phimdangchieu()
        {
            var PhimDangChieu = from tt in data.Movies
                                where tt.ComingSoon == 0
                                select tt;
            return View(PhimDangChieu);
        }
        [HttpGet]
        public ActionResult ThemPhim()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemPhim(IEnumerable<HttpPostedFileBase> files, Movy mv)
        {
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

                        mv.Image = "/Content/image/news/" + fileName;

                    }
                    if (count == 2)
                    {

                        mv.Banner = "/Content/image/news/" + fileName;
                    }
                }
                if (count == item.ContentLength)
                    break;
                count++;
            }
            data.Movies.InsertOnSubmit(mv);
            data.SubmitChanges();
            if(mv.ComingSoon == 1)
            {
                return RedirectToAction("phimSC");
            }
            else
            {
                return RedirectToAction("phimdangchieu");
            } 
        }
        //Them Chi Tiet DC
        public ActionResult ThemChiTiet(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemChiTiet(int id, FormCollection coll, MovieActor ma, MovieDirector md, MoviesGenre mg, ReviewOfMovie yt, YoutubeReview ytb)
        {
            var youtube = coll["Author"];
            var youtubelink = coll["Video"];
            //var actorid = data.Actors.First(a => a.Name == coll["Actor"]);
            //var drid = data.Directors.First(a => a.Name == coll["Director"]);
            //var gns = data.Genres.First(a => a.GenreName == coll["Type"]);
            var temp = 0;
            foreach(var i in data.YoutubeReviews)
            {
                if(i.YoutubeID > temp)
                {
                    temp = i.YoutubeID;
                }
            }
            int count = data.ReviewOfMovies.First(m => m.MovieID == id-1).YoutubeID;
            //
            //ma.MovieID = moviid.MovieID;
            //ma.ActorID = actorid.ActorID;
            //data.MovieActors.InsertOnSubmit(ma);
            ////
            //md.MovieID = moviid.MovieID;
            //md.DirectorID = drid.DirectorID;
            //data.MovieDirectors.InsertOnSubmit(md);
            ////
            //mg.MovieID = moviid.MovieID;
            //mg.GenreID = gns.GenreID;
            //data.MoviesGenres.InsertOnSubmit(mg);
            // Thêm Youtube Review
            ytb.Author = youtube;
            ytb.Image = "Image";
            ytb.Video = youtubelink;
            data.YoutubeReviews.InsertOnSubmit(ytb);
            // Thêm Review of Youtube
            yt.MovieID = id;
            yt.YoutubeID = temp+1;
            data.ReviewOfMovies.InsertOnSubmit(yt);
            data.SubmitChanges();
            if(data.Movies.First(m=>m.MovieID == id).ComingSoon == 1)
            {
                return RedirectToAction("phimSC");
            }
            return RedirectToAction("phimdangchieu");
        }
        // Phim sắp chiếu
        public ActionResult phimSC()
        {
            var phimSC = from tt in data.Movies
                         where tt.ComingSoon == 1 || tt.ComingSoon == null
                         select tt;
            return View(phimSC);
        }
        // Doanh thu vé
        public ActionResult datve()
        {
            var tk = from tt in data.Tickets
                         select tt;
            return View(tk);
        }
        public ActionResult chitietve(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("datve");
            }
            var chitiet = data.Tickets.Where(x => x.TicketID == id).Take(1).ToList();
            return View(chitiet);
        }
        public ActionResult XoaPhimSC(int id)
        {
            Movy dg = data.Movies.SingleOrDefault(n => n.MovieID == id);
            //ReviewOfMovie dg1 = data.ReviewOfMovies.First(n => n.MovieID == id);
            //YoutubeReview dg2 = data.YoutubeReviews.First(n => n.YoutubeID == dg1.YoutubeID);
            if (dg == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            //data.ReviewOfMovies.DeleteOnSubmit(dg1);
            //data.YoutubeReviews.DeleteOnSubmit(dg2);
            data.Movies.DeleteOnSubmit(dg);
            data.SubmitChanges();
            return RedirectToAction("PhimSC");
        }
        public ActionResult XoaPhimDC(int id)
        {
            Movy dg = data.Movies.SingleOrDefault(n => n.MovieID == id);
            if (dg == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            data.Movies.DeleteOnSubmit(dg);
            data.SubmitChanges();
            return RedirectToAction("PhimDC");
        }
    }
}
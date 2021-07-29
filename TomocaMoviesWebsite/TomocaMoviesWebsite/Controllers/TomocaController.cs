using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TomocaMoviesWebsite.Models;

namespace TomocaMoviesWebsite.Controllers
{
    public class TomocaController : Controller
    {
        // GET: Tomoca
        TomocaMoviesDataContext db = new TomocaMoviesDataContext();
        private List<Movy> TakeMovie(int count)
        {
            return db.Movies.OrderByDescending(a => a.MovieID).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var tm = TakeMovie(18);
            return View(tm);
        }

        public static string FilterSql(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            s = s.Trim().ToLower();
            s = s.Replace("=", "");
            s = s.Replace("'", "");
            s = s.Replace(";", "");
            s = s.Replace(" or ", "");
            s = s.Replace("select", "");
            s = s.Replace("update", "");
            s = s.Replace("insert", "");
            s = s.Replace("delete", "");
            s = s.Replace("declare", "");
            s = s.Replace("exec", "");
            s = s.Replace("drop", "");
            s = s.Replace("create", "");
            s = s.Replace("%", "");
            s = s.Replace("--", "");
            return s;
        }

        private List<Movy> GetMovies()
        {
            List<Movy> movy = new List<Movy>(db.Movies);
            return movy;
        }
        private List<Actor> GetActors()
        {
            List<Actor> actor = new List<Actor>();
            return actor;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var una = collection["Username"];
            var pwa = collection["Password"];
            var un = FilterSql(una);
            var pw = FilterSql(pwa);
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
                        @Session["UserName"] = tk.Username;
                        @Session["Permission"] = 1;
                        ViewBag.ThongBao = "Đăng nhập Amin thành công admin";
                        return RedirectToAction("Home", "Admin");
                    }
                    if (tk.Permission == false || tk.Permission == null)
                    {
                        @Session["UserName"] = tk.Username;
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

            return RedirectToAction("Login", "Users");
        }
        public ActionResult MovieReview(string search)
        {
            var links = from l in db.Movies // lấy toàn bộ liên kết
                        select l;
            string secureSearch = HttpUtility.HtmlEncode(search);
            if (!String.IsNullOrEmpty(secureSearch)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.Title.Contains(secureSearch)); //lọc theo chuỗi tìm kiếm
            }
            return View(links);
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            List<Movy> Movie = db.Movies.ToList();
            List<MoviesGenre> MoviesGenre = db.MoviesGenres.ToList();
            List<Genre> Genre = db.Genres.ToList();
            List<ReviewOfMovie> reviewOfMovies = db.ReviewOfMovies.ToList();
            List<YoutubeReview> youtubeReviews = db.YoutubeReviews.ToList();
            List<Comment> comments = db.Comments.ToList();
            List<MovieDirector> movieDirectors = db.MovieDirectors.ToList();
            List<Director> directors = db.Directors.ToList();
            List<Actor> actors = db.Actors.ToList();
            List<MovieActor> movieActors = db.MovieActors.ToList();
            var linkMulti = from a in Movie
                            join
            b in MoviesGenre on a.MovieID equals b.MovieID into table_1
                            from b in table_1
                            join c in Genre on b.GenreID equals c.GenreID into table_2
                            from c in table_2
                            join d in movieDirectors on b.MovieID equals d.MovieID into table_3
                            from d in table_3
                            join e in directors on d.DirectorID equals e.DirectorID
                            join f in movieActors on b.MovieID equals f.MovieID into table_4
                            from f in table_4
                            join g in actors on f.ActorID equals g.ActorID
                            join h in reviewOfMovies on b.MovieID equals h.MovieID into table_5
                            from i in table_5
                            join j in youtubeReviews on i.YoutubeID equals j.YoutubeID
                            select new infoMovie
                            {
                                MovieInf = a,
                                movieID = b,
                                Category = c,
                                director = e,
                                actors = g,
                                youtubeReview = j
                            };
            var img = linkMulti.ToList();
            var imgg = img.Find(m => m.MovieInf.MovieID == id);
            return View(imgg);
        }

        private List<New> GetNews(int count)
        {
            return db.News.OrderByDescending(a => a.CreateTime).Take(count).ToList();
        }
        public ActionResult MoviesLatestNews()
        {
            var gido = GetNews(9);
            return View(gido);
        }
        private List<New> GetTopNews(int count)
        {
            return db.News.OrderByDescending(a => a.ReadCount).Take(count).ToList();
        }

        public ActionResult MoviesHotNews()
        {
            var gido = GetTopNews(9);
            return View(gido);
        }
        //ticket

        public ActionResult Tickets(string daysd)
        {
            if (daysd == null)
            {
                var listMovies = db.MiAnLiens.Where(x => x.MalID > 0);
                var links = from l in listMovies
                            where l.Time >= DateTime.Now
                            select l;
                IList<City> lstcity = new List<City>();

                foreach (var item in links)
                {
                    foreach (var i in db.Cities)
                    {
                        if (item.MovieTheater.CityID == i.CityID)
                        {
                            lstcity.Add(i);
                        }
                    }
                }
                ViewBag.RAP = new SelectList(db.MovieTheaters.ToList().OrderBy(n => n.TheaterName), "TheaterID", "TheaterName");
                ViewBag.CITY = new SelectList(db.Cities.ToList().OrderBy(n => n.CityID), "CityID", "City1");
                ViewData["thongbaongay"] = DateTime.Now.ToString("dd/MM/yyyy");
                ViewData["lstcity"] = lstcity;
                return View(links);
            }
            else
            {
                var dayy = DateTime.Today.AddDays(int.Parse(daysd));
                var listMovies = db.MiAnLiens.Where(x => x.MalID > 0);
                var links = from l in listMovies
                            where l.Time >= dayy
                            select l;
                IList<City> lstcity = new List<City>();

                foreach (var item in links)
                {
                    foreach (var i in db.Cities)
                    {
                        if (item.MovieTheater.CityID == i.CityID)
                        {
                            lstcity.Add(i);
                        }
                    }
                }
                ViewData["thongbaongay"] = daysd.ToString();
                ViewData["lstcity"] = lstcity;
                return View(links);
            }
        }

        //detail news
        public ActionResult NewsDetail(int? id)
        {
            if (id == null)
                return HttpNotFound();
            List<User> users = db.Users.ToList();
            List<New> news = db.News.ToList();
            var multi = from a in users
                        join b in news on a.UserID equals b.UserID into table1
                        from b in table1

                        select new readNew
                        {
                            usersss = a,
                            newsss = b
                        };
            var rn = multi.ToList();
            var dn = rn.Find(a => a.newsss.NewsID == id);
            if (dn == null)
                return HttpNotFound();
            return View(dn);
        }

        //ddd
        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(SendMailDto sendMailDto)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                var from = "nodejsmailertest00@gmail.com";
                var to = "tomocamovie@gmail.com";
                const string Password = "442442aA!";
                string mail_subject = sendMailDto.Subject;
                string mail_message = "From: " + sendMailDto.Name + "\n";
                mail_message += "Email: " + sendMailDto.Email + "\n";
                mail_message += "Subject: " + sendMailDto.Subject + "\n";
                mail_message += "Message: " + sendMailDto.Message + "\n";

                var smtp = new SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(from, Password);
                    smtp.Timeout = 20000;
                }
                smtp.Send(from, to, mail_subject, mail_message);
                ViewBag.Message = "Mail sent.";
                ModelState.Clear();
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message.ToString();
            }
            return View();
        }
        public ActionResult TicketShow(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Tomoca");
            }
            else
            {
                var tks = from l in db.Tickets
                          where l.TicketID == id
                          select l;
                return View(tks);
            }
        }
        public ActionResult TicketBook()
        {
            if (Session["Username"] == null || Session["Username"].ToString() == "")
            {
                return RedirectToAction("Login", "Users");
            }
            var listMovies = db.MiAnLiens.Where(x => x.MalID > 0).ToList();
            IList<City> lstcity1 = new List<City>();

            foreach (var item in listMovies)
            {
                foreach (var i in db.Cities)
                {
                    if (item.MovieTheater.CityID == i.CityID)
                    {
                        lstcity1.Add(i);
                    }
                }
            }
            ViewData["lstcity"] = lstcity1;
            return View(listMovies);
        }
        [HttpPost]
        public ActionResult TicketBook(FormCollection coll)
        {
            if (Session["Username"] == null || Session["Username"].ToString() == "")
            {
                return RedirectToAction("Login", "Users");
            }
            string tenrap = coll["tenrap"].ToString();
            var idrap = db.MovieTheaters.First(a => String.Compare(a.TheaterName,tenrap,true) == 0);
            var luungay = coll["luungay"];
            var listMovies = db.MiAnLiens.Where(x => x.MalID > 0).ToList();
            var links = from l in listMovies
                        where int.Parse(l.Time.ToString("dd")) == int.Parse(luungay) && l.TheaterID == idrap.TheaterID
                        select l;
            if (links == null)
            {
                IList<City> lstcity1 = new List<City>();

                foreach (var item in links)
                {
                    foreach (var i in db.Cities)
                    {
                        if (item.MovieTheater.CityID == i.CityID)
                        {
                            lstcity1.Add(i);
                        }
                    }
                }
                ViewData["lstcity"] = lstcity1;
                return View();
            }
            return View(links);
        }
       
        [HttpPost]
        public ActionResult choose(FormCollection coll)
        {
            if (Session["Username"] == null || Session["Username"].ToString() == "")
            {
                return RedirectToAction("Login", "Users");
            }
            var malid = from l in db.MiAnLiens
                        where l.MalID == int.Parse(coll["idmal"])
                        select l;
            ViewData["chonghethuong"] = coll["chonghethuong"];
            ViewData["chonghevip"] = coll["chonghevip"];
            ViewData["tongtientien"] = coll["tongtientien"];
            return View(malid);
        }
        [HttpPost]
        public ActionResult pay(FormCollection coll, Ticket tk)
        {
            if (Session["Username"] == null || Session["Username"].ToString() == "")
            {
                return RedirectToAction("Login", "Users");
            }
            float tien = int.Parse(coll["tongtientien"]);
            int soghe = int.Parse(coll["soluongghe"]);
            int css = int.Parse(coll["idmal"]);
            var tenghe = coll["laytenghedat"];
            int chonghethuong = int.Parse(coll["chonghethuong"]);
            int chonghevip = int.Parse(coll["chonghevip"]);
            var ml = db.MiAnLiens.First(x => x.MalID == css);
            ml.A4 = coll["A4"]; ml.B2 = coll["B2"]; ml.F1 = coll["F1"]; ml.F15 = coll["F15"];
            ml.A5 = coll["A5"]; ml.B3 = coll["B3"]; ml.F2 = coll["F2"]; ml.F16 = coll["F16"];
            ml.A6 = coll["A6"]; ml.B4 = coll["B4"]; ml.F3 = coll["F3"];
            ml.A7 = coll["A7"]; ml.B5 = coll["B5"]; ml.F4 = coll["F4"];
            ml.A8 = coll["A8"]; ml.B6 = coll["B6"]; ml.F5 = coll["F5"];
            ml.A9 = coll["A9"]; ml.B7 = coll["B7"]; ml.F6 = coll["F6"];
            ml.A10 = coll["A10"]; ml.B8 = coll["B8"]; ml.F7 = coll["F7"];
            ml.A11 = coll["A11"]; ml.B9 = coll["B9"]; ml.F8 = coll["F8"];
            ml.A12 = coll["A12"]; ml.B10 = coll["B10"]; ml.F9 = coll["F9"];
            ml.A13 = coll["A13"]; ml.B11 = coll["B11"]; ml.F10 = coll["F10"];
            ml.B12 = coll["B12"]; ml.F11 = coll["F11"];
            ml.B14 = coll["B14"]; ml.B13 = coll["B13"]; ml.F12 = coll["F12"];
            ml.B15 = coll["B15"]; ml.F13 = coll["F13"]; ml.F14 = coll["F14"];
            ml.C2 = coll["C2"]; ml.D2 = coll["D2"]; ml.E2 = coll["E2"];
            ml.C3 = coll["C3"]; ml.D3 = coll["D3"]; ml.E3 = coll["E3"];
            ml.C4 = coll["C4"]; ml.D4 = coll["D4"]; ml.E4 = coll["E4"];
            ml.C5 = coll["C5"]; ml.D5 = coll["D5"]; ml.E5 = coll["E5"];
            ml.C6 = coll["C6"]; ml.D6 = coll["D6"]; ml.E6 = coll["E6"];
            ml.C7 = coll["C7"]; ml.D7 = coll["D7"]; ml.E7 = coll["E7"];
            ml.C8 = coll["C8"]; ml.D8 = coll["D8"]; ml.E8 = coll["E8"];
            ml.C9 = coll["C9"]; ml.D9 = coll["D9"]; ml.E9 = coll["E9"];
            ml.C10 = coll["C10"]; ml.D10 = coll["D10"]; ml.E10 = coll["E10"];
            ml.C11 = coll["C11"]; ml.D11 = coll["D11"]; ml.E11 = coll["E11"];
            ml.C12 = coll["C12"]; ml.D12 = coll["D12"]; ml.E12 = coll["E12"];
            ml.C13 = coll["C13"]; ml.D13 = coll["D13"]; ml.E13 = coll["E13"];
            ml.C14 = coll["C14"]; ml.D14 = coll["D14"]; ml.E14 = coll["E14"];
            ml.C15 = coll["C15"]; ml.D15 = coll["D15"]; ml.E15 = coll["E15"];
            tk.MalID = ml.MalID;
            tk.Money = tien;
            tk.Seat = tenghe;
            tk.Vip = chonghevip;
            tk.Normal = chonghethuong;
            tk.AmountSeats = soghe;
            var iduser = db.Users.First(x => x.Username == Session["Username"].ToString());
            tk.UserID = iduser.UserID;
            db.Tickets.InsertOnSubmit(tk);
            UpdateModel(ml);
            db.SubmitChanges();

            var mll = db.Tickets.OrderByDescending(x => x.TicketID).Take(1);
            return View(mll);
        }
    }
}
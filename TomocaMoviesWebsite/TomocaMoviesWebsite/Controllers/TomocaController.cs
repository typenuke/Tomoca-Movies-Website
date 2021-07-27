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
            var un = collection["Username"];
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
                        @Session["UserName"] = tk.Username;
                        @Session["Permission"] = 1;
                        ViewBag.ThongBao = "Đăng nhập thành công admin";
                        return RedirectToAction("Home", "Admin");
                    }
                    if (tk.Permission == false || tk.Permission == null)
                    {
                        @Session["UserName"] = tk.Username;
                        @Session["Permission"] = null;
                        ViewBag.ThongBao = "Đăng nhập thành công";
                        return RedirectToAction("Login", "Users");
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

            if (!String.IsNullOrEmpty(search)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.Title.Contains(search)); //lọc theo chuỗi tìm kiếm
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
        public ActionResult TicketShow()
        {
            return View();
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
        public ActionResult thankyou()
        {
            return View();
        }
        [HttpPost]
        public ActionResult choose(FormCollection coll)
        {
            var malid = db.MiAnLiens.First(x => x.MalID == int.Parse(coll["idmal"]));
            ViewData["chonghethuong"] = coll["chonghethuong"];
            ViewData["chonghevip"] = coll["chonghevip"];
            ViewData["tongtientien"] = coll["tongtientien"];
            return View(malid);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebXemPhim.Models;
using System.IO;
namespace WebXemPhim.Areas.Admin.Controllers
{
    public class DSPhimController : BaseController
    {
        //
        // GET: /Admin/DSPhim/

        DataClasses1DataContext data = new DataClasses1DataContext();
        public ActionResult DSPhimLe()
        {
            var ALL_Phim = from tt in data.DSPhimLes select tt;
            return View(ALL_Phim);
        }
        //DSPhim
        public ActionResult DSPhimBo()
        {

            //var ALL_Phim = from tt in data.DSPhimBoes select tt;
            var ALL_Phim = from tt in data.DSPhimBos select tt;
            return View(ALL_Phim);
        }
        //View 
        public ActionResult CTTapPhimBo(int id)
        {
            var ALL_Phim = from tt in data.CTTapPhims.Where(m => m.ID == id) select tt;
            ViewData["iD"] = id;
            return View(ALL_Phim);
        }

        //Thêm phim bộ
        [HttpGet]
        public ActionResult ThemPhimBo()
        {
            ViewData["NamPH"] = new SelectList(data.Nams, "MaNam", "TenNam");
            ViewData["IDTheLoai"] = new SelectList(data.TheLoais, "IDTheLoai", "TenTheLoai");
            ViewData["IDQuocGia"] = new SelectList(data.QuocGias, "MaQG", "TenQG");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemPhimBo(FormCollection collection, DSPhimBo pb, HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Bạn chưa chọn hình ";
            }
            else
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                fileUpload.SaveAs(path);
                //int theloai = int.Parse(collection["IDTheLoai"]);
                int qg = int.Parse(collection["IDQuocGia"]);
                int nam = int.Parse(collection["NamPH"]);
                pb.Img = fileName;
                pb.MaQG = qg;
                pb.NamPhatHanh = nam;
                pb.LuotXem = 1;
            }
            data.DSPhimBos.InsertOnSubmit(pb);
            data.SubmitChanges();
            return RedirectToAction("DSPhimBo");
        }
        //Xóa Phim
        public ActionResult XoaPhim(int id)
        {
            DSPhimBo tk = data.DSPhimBos.SingleOrDefault(n => n.ID == id);
            if (tk == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            data.DSPhimBos.DeleteOnSubmit(tk);
            data.SubmitChanges();
            return RedirectToAction("DSPhimBo");
        }
        //Sửa Phim
        public ActionResult SuaPhim(int id)
        {
            DSPhimBo phim = data.DSPhimBos.SingleOrDefault(n => n.ID == id);
            ViewData["NamPH"] = new SelectList(data.Nams, "MaNam", "TenNam", phim.NamPhatHanh);
            ViewData["IDTheLoai"] = new SelectList(data.TheLoais, "IDTheLoai", "TenTheLoai", phim.IDTheLoai);
            ViewData["IDQuocGia"] = new SelectList(data.QuocGias, "MaQG", "TenQG", phim.MaQG);
            if (phim == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(phim);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaPhim(int id, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            var phim = data.DSPhimBos.First(n => n.ID == id);
            var tenphim = collection["TenPhim"];
            int nam = int.Parse(collection["NamPH"]);
            var thoiluong = collection["ThoiLuong"];
            if (String.IsNullOrEmpty(tenphim))
            {
                ViewData["Loi"] = "Tên phim được để trống";
            }
            else
                if (String.IsNullOrEmpty(thoiluong))
                {
                    ViewData["Loi1"] = "Thời lượng được để trống";
                }
                else
                {
                    if (fileUpload == null)
                    {
                        ViewBag.ThongBao = "Bạn chưa chọn hình ";
                    }
                    else
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                        fileUpload.SaveAs(path);
                        phim.Img = fileName;
                    }

                    phim.NamPhatHanh = nam;
                    UpdateModel(phim);
                    data.SubmitChanges();
                    return RedirectToAction("DSPhimBo");
                }
            return this.SuaPhim(id);
        }
        //Xóa tập phim
        public ActionResult XoaTapPhim(int id)
        {
            CTTapPhim tk = data.CTTapPhims.SingleOrDefault(n => n.IDPhim == id);
            if (tk == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            data.CTTapPhims.DeleteOnSubmit(tk);
            data.SubmitChanges();
            return RedirectToAction("CTTapPhimBo", new { id = tk.ID});
        }
        //Thêm tập phim
        
        [HttpGet]
        public ActionResult ThemTapPhim(int id)
        {
            DSPhimBo phimbo = data.DSPhimBos.SingleOrDefault(m => m.ID == id);
            int taphientai = data.CTTapPhims.Where(m => m.ID == id).ToList().Count;
            ViewData["TapPhim"] = taphientai + 1;
            ViewData["TenPhim"] = phimbo.TenPhim;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemTapPhim(FormCollection collection, CTTapPhim pb,int id)
        {
            int idphim = id;
            int taphientai = data.CTTapPhims.Where(m=>m.ID==id).ToList().Count;
            int tapphim = taphientai+1;
            var linkphim = collection["Link"];

                pb.ID = idphim;
                pb.TapPhim = tapphim;
                pb.Link = linkphim;
            
            data.CTTapPhims.InsertOnSubmit(pb);
            data.SubmitChanges();
            return RedirectToAction("CTTapPhimBo", "DSPhim", new { id = idphim });
        }
        //Sửa Tập phim
        public ActionResult SuaTap(int id, int tapphim)
        {
            CTTapPhim phim = data.CTTapPhims.First(n => n.ID == id && n.TapPhim == tapphim);
            
            return View(phim);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaTap(int id, FormCollection collection, int tapphim)
        {

            var phim = data.CTTapPhims.First(n => n.ID == id && n.TapPhim == tapphim);
          

            var linkphim = collection["Link"];


            
            phim.Link = linkphim;
                    UpdateModel(phim);
                    data.SubmitChanges();
                    return RedirectToAction("CTTapPhimBo", "DSPhim", new { id = id });
            
        }
        //nội dung
        public ActionResult NoiDung()
        {

            return View();
        }
        //Thêm phim lẻ
        [HttpGet]
        public ActionResult ThemPhimLe()
        {
            ViewData["NamPH"] = new SelectList(data.Nams, "MaNam", "TenNam");
            ViewData["IDTheLoai"] = new SelectList(data.TheLoais, "IDTheLoai", "TenTheLoai");
            ViewData["IDQuocGia"] = new SelectList(data.QuocGias, "MaQG", "TenQG");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemPhimLe(FormCollection collection, DSPhimLe pb, HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Bạn chưa chọn hình ";
            }
            else
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                fileUpload.SaveAs(path);
                //int theloai = int.Parse(collection["IDTheLoai"]);
                int qg = int.Parse(collection["IDQuocGia"]);
                int nam = int.Parse(collection["NamPH"]);
                pb.Img = fileName;
                pb.MaQG = qg;
                pb.NamPhatHanh = nam;
                pb.LuotXem = 1;
            }
            data.DSPhimLes.InsertOnSubmit(pb);
            data.SubmitChanges();
            return RedirectToAction("DSPhimLe");
        }
        public ActionResult ChiTietPhimLe(int id)
        {
            DSPhimLe Phim = data.DSPhimLes.SingleOrDefault(m => m.ID == id);
            TheLoai tl = data.TheLoais.SingleOrDefault(m => m.IDTheLoai == Phim.IDTheLoai);
            Nam nam = data.Nams.SingleOrDefault(m => m.MaNam == Phim.NamPhatHanh);
            QuocGia qg = data.QuocGias.SingleOrDefault(m => m.MaQG == Phim.MaQG);
            ViewData["tl"] = tl.TenTheLoai;
            ViewData["nam"] = nam.TenNam;
            ViewData["quocgia"] = qg.TenQG;
            ViewData["iD"] = id;
            return View(Phim);
        }
        public ActionResult SuaPhimLe(int id)
        {
            DSPhimLe phim = data.DSPhimLes.SingleOrDefault(n => n.ID == id);
            ViewData["NamPH"] = new SelectList(data.Nams, "MaNam", "TenNam", phim.NamPhatHanh);
            ViewData["IDTheLoai"] = new SelectList(data.TheLoais, "IDTheLoai", "TenTheLoai", phim.IDTheLoai);
            ViewData["IDQuocGia"] = new SelectList(data.QuocGias, "MaQG", "TenQG", phim.MaQG);
            if (phim == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(phim);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaPhimLe(int id, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            var phim = data.DSPhimLes.First(n => n.ID == id);
            var tenphim = collection["TenPhim"];
            int nam = int.Parse(collection["NamPH"]);
            var thoiluong = collection["ThoiLuong"];
            if (String.IsNullOrEmpty(tenphim))
            {
                ViewData["Loi"] = "Tên phim được để trống";
            }
            else
                if (String.IsNullOrEmpty(thoiluong))
                {
                    ViewData["Loi1"] = "Thời lượng được để trống";
                }
                else
                {
                    if (fileUpload == null)
                    {
                        ViewBag.ThongBao = "Bạn chưa chọn hình ";
                    }
                    else
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                        fileUpload.SaveAs(path);
                        phim.Img = fileName;
                    }

                    phim.NamPhatHanh = nam;
                    UpdateModel(phim);
                    data.SubmitChanges();
                    return RedirectToAction("DSPhimLe");
                }
            return this.SuaPhimLe(id);
        }
    }
}

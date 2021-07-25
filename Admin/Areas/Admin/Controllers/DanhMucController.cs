using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebXemPhim.Models;

namespace WebXemPhim.Areas.Admin.Controllers
{
    public class DanhMucController : BaseController
    {
        //
        // GET: /Admin/DanhMuc/
        DataClasses1DataContext data = new DataClasses1DataContext();


        WebXemPhim.Entity.dbDoAnWebEntities db = new Entity.dbDoAnWebEntities();
        public ActionResult TheLoai()
        {
            var tl = data.TheLoais.ToList();
            return View(tl);
        }
        [HttpGet]
        public ActionResult ThemTheLoai()
        {
            return View();
        }
        //Thêm thể loai
        [HttpPost]
        public ActionResult ThemTheLoai(TheLoai tl, FormCollection coll)
        {

            var ten = coll["TenTheLoai"];
            var theloai = data.TheLoais.ToList();

            if (String.IsNullOrEmpty(ten))
                ViewData["Loi1"] = "Tên thể loại không được để chống";

            else
            {
                foreach (var item in theloai)
                {
                    if (String.Compare(item.TenTheLoai, ten, true) == 0)
                    {
                        ViewData["Loi2"] = "Đã có thể loại này";
                        return View();
                    }
                }
                tl.TenTheLoai = ten;
                data.TheLoais.InsertOnSubmit(tl);
                data.SubmitChanges();
                return RedirectToAction("TheLoai");
            }
            return View();
        }
        //Xóa thể loai
        public ActionResult XoaTheLoai(int id)
        {
            TheLoai tl = data.TheLoais.SingleOrDefault(n => n.IDTheLoai == id);
            if (tl == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            data.TheLoais.DeleteOnSubmit(tl);
            data.SubmitChanges();
            return RedirectToAction("TheLoai");
        }
        //Sửa thể loại
        public ActionResult SuaTheLoai(int id)
        {
            var tl = data.TheLoais.First(n => n.IDTheLoai == id);
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
            var tl = data.TheLoais.First(n => n.IDTheLoai == id);
            var ten = collection["TenTheLoai"];
            var theloai = data.TheLoais.ToList();
            foreach (var item in theloai)
            {
                if (String.Compare(item.TenTheLoai, ten, true) == 0 && item.IDTheLoai != id)
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
                tl.TenTheLoai = ten;
                UpdateModel(tl);
                data.SubmitChanges();
                return RedirectToAction("TheLoai");
            }
            return this.SuaTheLoai(id);
        }
        //-----------------------------------------Năm Phát Hành-------------------------------------------
        public ActionResult NamPhatHanh()
        {
            var nam = data.Nams.ToList();
            return View(nam);
        }

        [HttpGet]
        public ActionResult ThemNam()
        {
            return View();
        }
        //Thêm thể loai
        [HttpPost]
        public ActionResult ThemNam(Nam tl, FormCollection coll)
        {
            var ten = (coll["TenNam"]);
            var namph = data.Nams.ToList();

            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Loi1"] = "Năm phát hành không được để chống";
            }
            else
            {
                foreach (var item in namph)
                {
                    if (item.TenNam == int.Parse(ten))
                    {
                        ViewData["Loi2"] = "Đã có năm phát hành này";
                        return View();
                    }
                }
                tl.TenNam = int.Parse(ten);
                data.Nams.InsertOnSubmit(tl);
                data.SubmitChanges();
                return RedirectToAction("NamPhatHanh");
            }
            return View();
        }
        //Xóa thể loai
        public ActionResult XoaNam(int id)
        {
            Nam tl = data.Nams.SingleOrDefault(n => n.MaNam == id);
            if (tl == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            data.Nams.DeleteOnSubmit(tl);
            data.SubmitChanges();
            return RedirectToAction("NamPhatHanh");
        }
        //Sửa thể loại
        public ActionResult SuaNam(int id)
        {
            var tl = data.Nams.First(n => n.MaNam == id);
            if (tl == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(tl);
        }

        [HttpPost]
        public ActionResult SuaNam(int id, FormCollection collection)
        {
            var tl = data.Nams.First(n => n.MaNam == id);
            var namph = data.Nams.ToList();
            var ten = collection["TenNam"];
            if (string.IsNullOrEmpty(ten.ToString()))
            {
                ViewData["Loi1"] = "Năm phát hành không được để trống";
            }
            else
            {
                foreach (var item in namph)
                {
                    if (item.TenNam == int.Parse(ten) && item.MaNam != id)
                    {
                        ViewData["Loi2"] = "Đã có năm phát hành này";
                        return this.SuaNam(id);
                    }
                }
                tl.TenNam = int.Parse(ten);
                UpdateModel(tl);
                data.SubmitChanges();
                return RedirectToAction("NamPhatHanh");
            }
            return this.SuaNam(id);
        }
        //-----------------------------------------Quốc Gia--------------------------------------------
        public ActionResult QuocGia()
        {
            var qg = data.QuocGias.ToList();
            return View(qg);
        }
        [HttpGet]
        public ActionResult ThemQuocGia()
        {
            return View();
        }
        //Thêm thể loai
        [HttpPost]
        public ActionResult ThemQuocGia(QuocGia qg, FormCollection coll)
        {
            var ten = coll["TenQG"];
            var quocgia = data.QuocGias.ToList();

            if (String.IsNullOrEmpty(ten))
                ViewData["Loi1"] = "Tên quốc gia không được để chống";

            else
            {
                foreach (var item in quocgia)
                {
                    if (String.Compare(item.TenQG, ten, true) == 0)
                    {
                        ViewData["Loi2"] = "Đã có quốc gia này rồi !!!";
                        return View();
                    }
                }
                qg.TenQG = ten;
                data.QuocGias.InsertOnSubmit(qg);
                data.SubmitChanges();
                return RedirectToAction("QuocGia");
            }
            return View();
        }
        //Xóa thể loai
        public ActionResult XoaQuocGia(int id)
        {
            QuocGia qg = data.QuocGias.SingleOrDefault(n => n.MaQG == id);
            if (qg == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            data.QuocGias.DeleteOnSubmit(qg);
            data.SubmitChanges();
            return RedirectToAction("QuocGia");
        }
        //Sửa thể loại
        public ActionResult SuaQuocGia(int id)
        {
            var qg = data.QuocGias.First(n => n.MaQG == id);
            if (qg == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(qg);
        }

        [HttpPost]
        public ActionResult SuaQuocGia(int id, FormCollection collection)
        {
            var qg = data.QuocGias.First(n => n.MaQG == id);
            var ten = collection["TenQG"];
            var quocgia = data.QuocGias.ToList();
            foreach (var item in quocgia)
            {
                if (String.Compare(item.TenQG, ten, true) == 0 && item.MaQG != id)
                {
                    ViewData["Loi2"] = "Quốc gia này đã tồn tại";
                    return this.SuaQuocGia(id);
                }
            }
            if (String.IsNullOrEmpty(ten))
            {
                ViewData["Loi1"] = "Tên quốc gia không được để trống";
            }
            else
            {
                qg.TenQG = ten;
                UpdateModel(qg);
                data.SubmitChanges();
                return RedirectToAction("QuocGia");
            }
            return this.SuaQuocGia(id);
        }


        //-------------------------------------------BANNER------------------------------------------------------
        public ActionResult Banner()
        {
            var ALL_Phim = from tt in data.Banners select tt;
            return View(ALL_Phim);
        }
        //Sửa Hình banner
        public ActionResult SuaBanner(int id)
        {
            Banner banner = data.Banners.SingleOrDefault(n => n.ID == id);
            if (banner == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            ViewData["IDPhim"] = new SelectList(data.DSPhimBos, "ID", "TenPhim", banner.IDPhim);
            return View(banner);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaBanner(int id, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            var banner = data.Banners.First(n => n.ID == id);
            var kt = data.Banners.ToList();
            int IDPhim = int.Parse(collection["IDPhim"]);
            if (fileUpload == null)
            {
                foreach (var item in kt)
                {
                    if (IDPhim == item.IDPhim && item.ID != id)
                    {
                        ViewBag.ThongBao = "Phim này đã có trong banner";
                        return this.SuaBanner(id);
                    }
                }
                UpdateModel(banner);
                data.SubmitChanges();
            }
            else
            {
                if (ModelState.IsValid)
                {

                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    foreach (var item in kt)
                    {
                        if (String.Compare(item.Img, fileName, true) == 0 && item.ID != id)
                        {
                            ViewBag.ThongBao = "Banner đã có hình này";
                            return this.SuaBanner(id);
                        }
                    }
                    fileUpload.SaveAs(path);
                    banner.Img = fileName;
                    UpdateModel(banner);
                    data.SubmitChanges();
                }
            }
            return RedirectToAction("Banner");
        }



        //------------------------------------------------------------
        public ActionResult TinTuc()
        {
            var tt = data.tintucphims.ToList();
            return View(tt);
        }
        public ActionResult SuaTinTuc(int id)
        {
            tintucphim tin = data.tintucphims.SingleOrDefault(n => n.idtintuc == id);
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
            var tin = data.tintucphims.First(n => n.idtintuc == id);
            var kt = data.tintucphims.ToList();
            if (fileUpload != null)
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                foreach (var item in kt)
                {
                    if (String.Compare(item.hinhanh, fileName, true) == 0 && item.idtintuc != id)
                    {
                        ViewBag.ThongBao = "Banner đã có hình này";
                        return this.SuaBanner(id);
                    }
                }
                fileUpload.SaveAs(path);
                tin.hinhanh = fileName;

            }
            UpdateModel(tin);
            data.SubmitChanges();
            return RedirectToAction("TinTuc");
        }
        public ActionResult XoaTinTuc(int id)
        {
            tintucphim tt = data.tintucphims.SingleOrDefault(n => n.idtintuc == id);
            if (tt == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            data.tintucphims.DeleteOnSubmit(tt);
            data.SubmitChanges();
            return RedirectToAction("TinTuc");
        }

        public ActionResult ThemTinTuc()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemTinTuc(tintucphim tt, FormCollection coll, HttpPostedFileBase fileUploadTT)
        {
            var ten = coll["tieude"];
            var tomtat = coll["tomtat"];
            var nd = coll["noidung"];
            //var hinh = coll["hinh"];
            var fileName = Path.GetFileName(fileUploadTT.FileName);
            var ngaycn = coll["ngaycapnhat"];
            int lx = 1;
            var tintuc = data.tintucphims.ToList();
            if(string.IsNullOrEmpty(tomtat))
            {
                ViewData["Loi1"] = "Tóm tắt không được để trống";
                return View();
                }
            if (string.IsNullOrEmpty(nd))
            {
                ViewData["Loi2"] = "Nội dung không được để trống";
                return View();
            }
            
            if (String.IsNullOrEmpty(ten))
                ViewData["Loi3"] = "Tiêu đề không được để trống";
            else
            {
                foreach (var item in tintuc)
                {
                    if (String.Compare(item.tieude, ten, true) == 0)
                    {
                        ViewData["Loi4"] = "Tin tức này có rồi!!!";
                        return View();
                    }
                }
            }
            if (fileUploadTT == null)
            {
                ViewBag.ThongBao = "Bạn chưa chọn hình ";
            }
            else
            {
                
                var pathtt = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                fileUploadTT.SaveAs(pathtt);
                tt.tieude = ten;
                DateTime a = DateTime.Now;
                tt.hinhanh = fileName;  
                tt.ngaycapnhat = a;
                tt.luotxem = lx;
                data.tintucphims.InsertOnSubmit(tt);
                data.SubmitChanges();
            }
            return RedirectToAction("TinTuc");
        }

        public ActionResult GioiThieuWebXemPhimSo1VietNam()
        {
            var tt = data.gioithieus.ToList();
            return View(tt);   
        }
        public ActionResult SuaGioiThieu(int id)
        {
            var tin = data.gioithieus.First(n => n.idgioitin == id);
            if (tin == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            return View(tin);
        }

        [HttpPost]
        public ActionResult SuaGioiThieu(int id, FormCollection collection)
        {
            var tin = data.gioithieus.First(n => n.idgioitin == id);
            var nd = collection["noidung"];
            var sdt = collection["sdtlien"];
            tin.noidung = nd;
            tin.sdtlien = sdt;
            UpdateModel(tin);
            data.SubmitChanges();
            return RedirectToAction("GioiThieuWebXemPhimSo1VietNam");
        }
       
    }
}


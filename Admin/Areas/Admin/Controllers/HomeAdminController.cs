using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebXemPhim.Models;

namespace WebXemPhim.Areas.Admin.Controllers
{
    public class HomeAdminController : BaseController
    {
        //
        // GET: /Admin/HomeAdmin/
        DataClasses1DataContext data = new DataClasses1DataContext();
        public ActionResult Home()
        {                 
            int demnd = data.TaiKhoans.ToList().Count;
            int demphimle = data.DSPhimLes.ToList().Count;
            int demphimbo = data.DSPhimBos.ToList().Count;
            ViewData["NguoiDung"] = demnd;
            ViewData["PhimLe"] = demphimle;
            ViewData["PhimBo"] = demphimbo;
            var tl = data.TheLoais.ToList();
            var nam = data.Nams.ToList();            
            ViewData["TheLoai"] = tl;
            ViewData["Nam"] = nam;
            var DSPhimBo = data.DSPhimBos.OrderByDescending(x => x.LuotXem).Take(10).ToList();
            ViewData["TopPhim"] = DSPhimBo;
            var DSPhimLe = data.DSPhimLes.OrderByDescending(x => x.LuotXem).Take(10).ToList();
            ViewData["TopPhimLe"] = DSPhimLe;
            var DSPhimBoMoi = data.DSPhimBos.OrderByDescending(a => a.ID).Take(10).ToList();
            ViewData["PhimBoMoi"] = DSPhimBoMoi;
            var DSPhimLeMoi = data.DSPhimLes.OrderByDescending(a => a.ID).Take(10).ToList();
            ViewData["PhimLeMoi"] = DSPhimLeMoi;
            return View();
        }
    }
}

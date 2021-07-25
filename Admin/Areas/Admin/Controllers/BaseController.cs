using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebXemPhim.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Admin/Base/

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var a = Session["quyen"];
            if (a == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "Index", Area = ""}));
            }
            base.OnActionExecuting(filterContext);
        }


    }
}

using Beautopia.Model.Area;
using Beautopia.web.Areas.Admin.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beautopia.web.Areas.Admin.Filters
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            var url = filterContext.HttpContext.Request.Path;
            //var Controller = (string)filterContext.RouteData.Values["Controller"];
            // var Action = (string)filterContext.RouteData.Values["Action"];

            //var canAcess = false;

            //// check the refer
            //var referer = filterContext.HttpContext.Request.Headers["Referer"].ToString();
            //if (!string.IsNullOrEmpty(referer))
            //{
            //    var rUri = new System.UriBuilder(referer).Uri;
            //    var req = filterContext.HttpContext.Request;
            //    if (req.Host.Host == rUri.Host && req.Host.Port == rUri.Port && req.Scheme == rUri.Scheme)
            //    {
            //        canAcess = true;
            //    }
            //}
            //if (!canAcess)
            //{
            //    // filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Accounts", action = "NotAuthorize", area = "" }));
            //    //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", area = "" }));
            //}


            var SessionValues = session.GetObjectFromJson<UserLogin>("Login");

            if (SessionValues == null)
            {
                filterContext.Result = new RedirectResult("~/Admin");
                // GoToLoginPage(filterContext);
                return;
            }
            else {

                var Menus = SessionValues.Menus.Where(a => a.MenuUrl == url).FirstOrDefault();
                if (Menus == null) {
                    filterContext.Result = new RedirectResult("~/Admin");
                    // GoToLoginPage(filterContext);
                    return;
                }

            }
           

            base.OnActionExecuting(filterContext);
        }

        private static void GoToLoginPage(ActionExecutingContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
          new RouteValueDictionary
   {
                { "controller", "Accounts" },
                { "action", "Login" } ,
                { "Area","Admin" },
                {"ReturnUrl",""
    },

   });
        }

    }
}

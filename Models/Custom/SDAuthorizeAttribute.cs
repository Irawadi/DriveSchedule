using System.Web;
using System.Web.Mvc;
using AppointmentK1.Models;
using System.Web.Routing;

namespace AppointmentK1.Models.Custom
{
    public class SDAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                Student student = (Student)httpContext.Session["Student"];
                if (student.IsAuthorized)
                {
                    return true;
                }
                else
                {
                    httpContext.Items["redirectToLogIn"] = true;
                    return false;
                }
            }
            catch (System.Exception)
            {
                httpContext.Items["redirectToLogIn"] = true;
                return false;                
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Items.Contains("redirectToLogIn"))
            {
                var routeValues = new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index",
                });
                filterContext.Result = new RedirectToRouteResult(routeValues);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
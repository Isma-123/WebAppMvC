
using System.Web.Mvc;
using System.Web.Services.Description;

using WebApp.Models;
using WebApp.Controllers;
using System.Web;

namespace WebApp.Filters
{
    public class CheckSeccion : ActionFilterAttribute
    {


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
             
            var user = (USER)HttpContext.Current.Session["Usuario"];

            if(user is null)
            {
                if (filterContext.Controller is AccessController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Access/Login");
                    
                }
            }
            else
            {
                if(filterContext.Controller is AccessController == true)
                {

                    filterContext.HttpContext.Response.Redirect("~/Access/Index");
                }
            }
                base.OnActionExecuting(filterContext);
        }
 
    }
}
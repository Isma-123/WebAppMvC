
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class CloseSeccionController : Controller
    {
        // GET: CloseSeccion
        public ActionResult LogOff()
        {

            Session["Usuario"] = null;
            return RedirectToAction("Login", "Access");

        }
    }
}
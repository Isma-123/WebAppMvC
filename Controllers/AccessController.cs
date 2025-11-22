using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Enter(string user, string password)
        {
            try
            {
                using (DBMVCEntities db = new DBMVCEntities())
                {
                    var _read = db.USERS.Where(d => d.Email == user && d.Password
                    == password);


                    if (_read.Count() > 0)
                    {
                        Session["Usuario"] = _read.First(); // crea la sesión
                        return Content("1");
                    }
                    else
                    {
                        return Content("Ocurrió un error! Revisa el usuario y la clave.");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error menssage"] = "erro intentelo de nuevo por favor!";
                return Content("Ocurrió un error en el sistema: " + ex.Message);
            }
        }
    }
}

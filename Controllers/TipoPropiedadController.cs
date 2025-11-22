
using System.Collections.Generic;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Models.ViewModels;
using WebApp.Metodos;
using WebApp.Filters;

namespace WebApp.Controllers
{
    public class TipoPropiedadController : Controller
    {


        // GET: TipoPropiedadControlle 

        public ActionResult TipoPropiedad()
        {
            if (Session["Usuario"] is null)
                return RedirectToAction("Index", "Login");

            var model = TipoPropiedadMetodo.Instance.Listar();
            return View(model);
        } 



        [HttpGet] 
        public JsonResult ListTypeProperty()
        {
            var ols = new List<TipoPropiedad>();
            ols = TipoPropiedadMetodo.Instance.Listar();
            return Json(new { data = ols }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveTypeProperty(TipoPropiedad obj)
        {
           bool rs = false;


            rs = (obj.IdTipoPropiedad == 0) ? TipoPropiedadMetodo.Instance.Register(obj)
                  : TipoPropiedadMetodo.Instance.Modificar(obj);

          return Json (new {date = obj}, JsonRequestBehavior.AllowGet);
        }


        [HttpDelete]
        public JsonResult RemoveTypeProperty(int _id)
        {
            bool rs = false;
            rs = TipoPropiedadMetodo.Instance.Eliminar(_id);
            return Json(new { date = _id }, JsonRequestBehavior.AllowGet);
        }
    }
}
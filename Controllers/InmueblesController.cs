using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Metodos;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class InmueblesController : Controller
    {

        public ActionResult Index()  // genera la vista de los inmuebles
        {
            ViewBag.Menssage = "Listadod de Inmuebles";
            return View();
        }

        [HttpGet]

        public JsonResult Listar()
        {
            var rs = new List<Inmueble>();

            if(rs == null)
            {
                TempData["Menssage"] = "Error looking for the Json";
                return Json(new { date = TempData }, JsonRequestBehavior.AllowGet);
            }

            rs = InmueblesMetodo.Instance.Listar(); 
            return Json(new { date = rs }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost] 

        public JsonResult Post(Inmueble obj)
        {
            bool rs = true;

            if (!ModelState.IsValid)
            {
                TempData["menssage"] = "Erro encontrado el valor ";
                return Json(new { date = TempData }, JsonRequestBehavior.AllowGet);
            }

            if(!rs)
            {
                TempData["menssage"] = "Erro encontrado el valor ";
                return Json(new { date = TempData }, JsonRequestBehavior.AllowGet);
            }

            rs = (obj.IdInmueble == 0) ? InmueblesMetodo.Instance.Register(obj) :
            InmueblesMetodo.Instance.Modify(obj);
            return Json(new { date = rs }, JsonRequestBehavior.AllowGet);

        }


        [HttpDelete] 

        public JsonResult Delete(int id)
        {  

            bool rs = true;

            try
            {
                rs = InmueblesMetodo.Instance.Remove(id);
                return Json(new { date = id }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                TempData["Menssage error"] = ex.Message;
                return Json(new { date = TempData }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}
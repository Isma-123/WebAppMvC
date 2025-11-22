using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Metodos;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class CondicionController : Controller
    {
        // GET: Condicion 


        public ActionResult Listar()
        {
            return View();
        }


        [HttpGet] 

        public JsonResult ListCondition()
        {
            var ls = new List<Condicion>();

            ls = CondicionMetodo.Instance.Listar();
            return Json (new {data =  ls}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult PostCondition(Condicion obj)
        {

            bool rs = false;
            if (!ModelState.IsValid)
            {

                return Json(new { date = rs }, JsonRequestBehavior.AllowGet);

            }

            try
            {

                rs = (obj.IdCondicion == 0) ? CondicionMetodo.Instance.Register(obj) :
                CondicionMetodo.Instance.Modificar(obj);
            }
            catch (Exception ex)
            {

                TempData["menssage"] = $"errr postiong new condicio {ex.Message}";
                return Json(new { data = ex.Message, }, JsonRequestBehavior.AllowGet);


            }   


            return Json(new { data = rs },  JsonRequestBehavior.AllowGet); 


        }

           

        [HttpGet]   
        public JsonResult Eliminar(int id)
        {
            bool rs = false;

            rs = CondicionMetodo.Instance.Eliminar(id);  /// eliminar recurso 
            return Json(new { id = rs }, JsonRequestBehavior.AllowGet);

        }   
    }
}
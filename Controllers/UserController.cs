using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        // Mantengo tu Query tal cual
        public ActionResult Query()
        {
            List<QueryViewModels> ls = null;

            using (DBMVCEntities db = new DBMVCEntities())
            {
                ls = (from a in db.USERS
                      where a.idEstatus == 1
                      orderby a.Email
                      select new QueryViewModels
                      {
                          _Email = a.Email,
                          _Edad = a.Edad,
                          _Id = a.ID
                      }).ToList();

                return View(ls);
            }
        }

        // ================== CREATE ==================
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using (DBMVCEntities db = new DBMVCEntities())
                {
                    USER user = new USER
                    {
                        Nombre = model._Nombre,
                        Email = model._Email,
                        Edad = model._Edad,
                        Password = model._Clave,
                        idEstatus = 1
                    };

                    db.USERS.Add(user);
                    db.SaveChanges();
                }

                TempData["SuccessMessage"] = "Usuario registrado correctamente.";
                return RedirectToAction(Url.Content("/User/Query"));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error registrando usuario: " + ex.Message;
                return View(model);
            }
        }

        // ================== EDIT ==================
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (DBMVCEntities db = new DBMVCEntities())
            {
                var user = db.USERS.Find(id);
                if (user == null)
                    return HttpNotFound();

                // Llenamos el ViewModel con los datos existentes
                EditUserViewModel model = new EditUserViewModel
                {
                    _Id = user.ID,
                    _Nombre = user.Nombre,
                    _Email = user.Email,
                    _Edad = user.Edad
                };

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                using (DBMVCEntities db = new DBMVCEntities())
                {
                    var user = db.USERS.Find(model._Id);
                    if (user == null)
                        return HttpNotFound();

                    // Actualizamos los campos
                    user.Nombre = model._Nombre;
                    user.Email = model._Email;
                    user.Edad = model._Edad;

                    db.SaveChanges();
                }

                TempData["SuccessMessage"] = "Usuario actualizado correctamente.";
                return RedirectToAction(Url.Content("/User/Query"));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error actualizando usuario: " + ex.Message;
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (DBMVCEntities db = new DBMVCEntities())
            {
                var user = db.USERS.Find(id);
                if (user == null)
                {
                    TempData["ErrorMessage"] = "Usuario no encontrado.";
                    return RedirectToAction("Query");
                }

                var model = new EditUserViewModel
                {
                    _Id = user.ID,
                    _Nombre = user.Nombre,
                    _Email = user.Email,
                    _Edad = user.Edad
                };

                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int _Id)
        {
            try
            {
                using (DBMVCEntities db = new DBMVCEntities())
                {
                    var user = db.USERS.Find(_Id);
                    if (user != null)
                    {
                        db.USERS.Remove(user);
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "Usuario eliminado correctamente.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Usuario no encontrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error eliminando usuario: " + ex.Message;
            }

            return RedirectToAction("Query");
        }


    }
}

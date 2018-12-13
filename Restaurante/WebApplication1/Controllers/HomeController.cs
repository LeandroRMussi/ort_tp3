using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Mesa()
        {
            if (Session["IdUsuario"] == null)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult Menu(Models.ReservaMesa reservamesa)
        {
            if (Session["IdUsuario"] != null)
            {
                Session["Comensales"] = reservamesa.Comensales.ToString();
                Session["Fecha"] = reservamesa.Fecha.ToString();
                Session["Horainicio"] = reservamesa.Horainicio.ToString();
                Session["Mensaje"] = reservamesa.Descripcion.ToString();
                return View();
            }
        
            return View();
        }

        public ActionResult Login()
        {
            if (Session["IdUsuario"] != null)
            {
                return RedirectToAction("Mesa");
            }
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(PerfilUsuario newUser)
        {
            restauranteEntities db = new restauranteEntities();
            var obj = db.PerfilUsuario.Where(a => a.Usuario.Equals(newUser.Usuario) && a.Password.Equals(newUser.Password)).FirstOrDefault();
            if (obj == null) { 
                newUser.IsActive = false;
                newUser.TS = DateTime.Now;
                db.PerfilUsuario.Add(newUser);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("RegistroFail", "Error, el usuario ya existe en el sistema");
            return View(newUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(PerfilUsuario objUser)
        {
            if (ModelState.IsValid)
            {
                using (restauranteEntities db = new restauranteEntities())
                {
                    var obj = db.PerfilUsuario.Where(a => a.Usuario.Equals(objUser.Usuario) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.IsActive = true;
                        db.SaveChanges();

                        
                        Session["Email"] = obj.Email.ToString();
                        Session["IdUsuario"] = obj.IdUsuario.ToString();
                        Session["Usuario"] = obj.Usuario.ToString();
                        Session["isActive"] = obj.IsActive;
                        FormsAuthentication.SetAuthCookie(Session["Usuario"].ToString(), true);
                        return RedirectToAction("Mesa");
                        
                    } else
                    {
                        ModelState.AddModelError("LoginFail", "Error, datos incorrectos");
                        return View();
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["IdUsuario"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff(PerfilUsuario objUser)
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}
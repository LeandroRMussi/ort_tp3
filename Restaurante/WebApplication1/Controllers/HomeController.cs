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
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            if (Session["Usuario"] != null)
            {
                return RedirectToAction("UserDashBoard");
            }
            return View();
        }

        public ActionResult Registro()
        {
            return View();
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

                        Session["IdUsuario"] = obj.IdUsuario.ToString();
                        Session["Usuario"] = obj.Usuario.ToString();
                        Session["isActive"] = obj.IsActive;
                        FormsAuthentication.SetAuthCookie(Session["Usuario"].ToString(), true);
                        return RedirectToAction("UserDashBoard");
                        
                    } else
                    {
                        ModelState.AddModelError("LoginFail", "Datos incorrectos");
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
        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}
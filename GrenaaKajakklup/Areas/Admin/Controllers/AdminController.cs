using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RepoGKK.Factories;
using RepoGKK.Models.BaseModels;


namespace GrenaaKajakklup.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(String Name, String Password)
        {
           Gkkbruger Bruger = new Gkkbruger();
           BrugerFac BF = new BrugerFac();

            Bruger= BF.Login(Name, Password);
           // Bruger = BF.Login(Name, FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "sha1"));

            if (Bruger.ID > 0)
            {
                Session["userid"] = Bruger.ID;
                Session.Timeout = 120;
                return View("Index");
            }
            else
            {
                 ViewBag.MSG = "Brugernavn eller Password er forkert!";

            }
            
            return View("UserLogin");
        }
        public ActionResult UserLogin()
        {
            return View();
        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using RepoGKK.Factories;
using RepoGKK.Models.BaseModels;


namespace GrenaaKajakklup.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            GkkRedigerFac Redigerside = new GkkRedigerFac();
            return View(Redigerside.Get(1));
        }

        [HttpPost]
        public ActionResult Login(String Name, String Password)
        {
            Gkkbruger Bruger = new Gkkbruger();
            BrugerFac BF = new BrugerFac();

            Bruger = BF.Login(Name, Password);
            // Bruger = BF.Login(Name, FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "sha1"));

            if (Bruger.ID > 0)
            {
                Session["userid"] = Bruger.ID;
                Session.Timeout = 120;
                return View("Index");
            }
            else
            {
                ViewBag.MSG = "brugern blev ikke fundet!";
            }

            return View("UserLogin");
        }

        public ActionResult UserLogin()
        {
            return View();
        }


        //-------------------------------------------------------//


        public ActionResult OpretBruger()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OpretBruger(String Name, String Password)
        {
            OpretFac Kf = new OpretFac();
            Gkkbruger K = new Gkkbruger();

            if (Kf.UserExists(Name) != true)
            {
                if (Name != null || Password != null)
                {
                    K.Name = Name;
                    K.Password = Password;
                    Kf.Insert(K);
                    ViewBag.MSG = "brugern er blevet  oprettet";
                }
                else
                {
                    ViewBag.MSG = "felterne skal udfyldes";
                }
            }
            else
            {
                ViewBag.MSG = "Fejl brugeren eksiterer allerede";
            }

            return View("OpretBruger");
        }
        public ActionResult SletBruger(int id)
        {

            OpretFac Kf = new OpretFac();
            Gkkbruger K = new Gkkbruger();

            Kf.Delete(id);

            return View("Brugerliste", Kf.GetAll());
        }


        //-------------------------------------------------------//


        public ActionResult Brugerliste()
        {
            BrugerFac BF = new BrugerFac();

            return View(BF.GetAll());
        }


        //-------------------------------------------------------//


        public ActionResult ForsideRediger()
        {
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            return View(RedigerSide.Get(1));
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ForsideRediger(GkkRediger RedigerForside)
        {
            RedigerForside.Overskrift = " ";
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
           RedigerSide.Update(RedigerForside);
            return View(RedigerSide.Get(1));
        }

    }


}





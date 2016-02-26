using System;
using System.Collections.Generic;
using System.IO;
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
        Uploader u = new Uploader();
        GkkSlidderFac Slider = new GkkSlidderFac();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            GkkRedigerFac Redigerside = new GkkRedigerFac();
            return View(Redigerside.Get(1));
        }

        [HttpPost]
        public ActionResult Login(String Name, String Password)
        {
            GkkRedigerFac Redigerside = new GkkRedigerFac();
            Gkkbruger Bruger = new Gkkbruger();
            BrugerFac BF = new BrugerFac();


            Bruger = BF.Login(Name, Password);
            // Bruger = BF.Login(Name, FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "sha1"));

            if (Bruger.ID > 0)
            {
                Session["userid"] = Bruger.ID;
                Session.Timeout = 120;
                return View("Index", Redigerside.Get(1));
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
            GkkSlidderFac Redigerslidder = new GkkSlidderFac();
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

        //TJEK
        [OutputCache(NoStore = true, Duration = 0)]
        [HttpPost]
        public ActionResult Slider_Billede1(HttpPostedFileBase file)
        {
            GkkSlidder sliderBillede = Slider.Get(1);

            if (file != null)
            {
                string path = Request.PhysicalApplicationPath + "Areas/Admin/Pic/";
                System.IO.File.Delete(path + sliderBillede.Billedenavn);
                sliderBillede.Billedenavn = Path.GetFileName(u.UploadImage(file, path, 1170, true));
            }

            Slider.Update(sliderBillede);
            ViewBag.MSG = "Der er nu lavet";
            return RedirectToAction("ForsideRediger");
        }
        [OutputCache(NoStore = true, Duration = 0)]
        [HttpPost]
        public ActionResult Slider_Billede2(HttpPostedFileBase file)
        {
            GkkSlidder sliderBillede = Slider.Get(2);

            if (file != null)
            {
                string path = Request.PhysicalApplicationPath + "Areas/Admin/Pic/";
                System.IO.File.Delete(path + sliderBillede.Billedenavn);
                sliderBillede.Billedenavn = Path.GetFileName(u.UploadImage(file, path, 1170, true));
            }

            Slider.Update(sliderBillede);
            ViewBag.MSG = "Der er nu lavet";
            return RedirectToAction("ForsideRediger");
        }
        [OutputCache(NoStore = true, Duration = 0)]
        [HttpPost]
        public ActionResult Slider_Billede3(HttpPostedFileBase file)
        {
            GkkSlidder sliderBillede = Slider.Get(3);

            if (file != null)
            {
                string path = Request.PhysicalApplicationPath + "Areas/Admin/Pic/";
                System.IO.File.Delete(path + sliderBillede.Billedenavn);
                sliderBillede.Billedenavn = Path.GetFileName(u.UploadImage(file, path, 1170, true));
            }

            Slider.Update(sliderBillede);
            ViewBag.MSG = "Der er nu lavet";
            return RedirectToAction("ForsideRediger");
        }



        public ActionResult ROmedOSRediger()
        {
            GkkSlidderFac Redigerslidder = new GkkSlidderFac();
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            return View(RedigerSide.Get(5));
           
            
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ROmedOSRediger(GkkRediger RedigerForside)
        {
            RedigerForside.Overskrift = " ";
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            RedigerSide.Update(RedigerForside);
            return View(RedigerSide.Get(5));
        }





        public ActionResult VinterRoningRediger()
        {
           
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            return View(RedigerSide.Get(6));


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult VinterRoningRediger(GkkRediger RedigerForside)
        {
            RedigerForside.Overskrift = " ";
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            RedigerSide.Update(RedigerForside);
            return View(RedigerSide.Get(6));
        }
        public ActionResult TiderogPriserRediger()
        {
           
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            return View(RedigerSide.Get(7));


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TiderogPriserRediger(GkkRediger RedigerForside)
        {
            RedigerForside.Overskrift = " ";
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            RedigerSide.Update(RedigerForside);
            return View(RedigerSide.Get(7));
        }
        public ActionResult BegivenhederRediger()
        {

            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            return View(RedigerSide.Get(9));


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult BegivenhederRediger(GkkRediger RedigerForside)
        {
            RedigerForside.Overskrift = " ";
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            RedigerSide.Update(RedigerForside);
            return View(RedigerSide.Get(9));
        }
        public ActionResult KlubaftenRediger()
        {

            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            return View(RedigerSide.Get(8));


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult KlubAftenRediger(GkkRediger RedigerForside)
        {
            RedigerForside.Overskrift = " ";
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            RedigerSide.Update(RedigerForside);
            return View(RedigerSide.Get(8));
        }
        public ActionResult NyeBegivenhederRediger()
        {

            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            return View(RedigerSide.Get(10));


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult NyeBegivenhederRediger(GkkRediger RedigerForside)
        {
            RedigerForside.Overskrift = " ";
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            RedigerSide.Update(RedigerForside);
            return View(RedigerSide.Get(10));
        }

    }


}





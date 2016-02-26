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
        //--------------------------------Index start--------------------------------//

        public ActionResult Index()
        {
            GkkRedigerFac Redigerside = new GkkRedigerFac();
            return View(Redigerside.Get(1));
        }

        //--------------------------------index slut--------------------------------//

        //--------------------------------Login start--------------------------------//

        [HttpPost]
        public ActionResult Login(String Name, String Password)
        {
            GkkRedigerFac Redigeresiden = new GkkRedigerFac();
            Gkkbruger Bruger = new Gkkbruger();
            BrugerFac BF = new BrugerFac();

            Bruger = BF.Login(Name, Password);
            // Bruger = BF.Login(Name, FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "sha1"));

            if (Bruger.ID > 0)
            {
                Session["userid"] = Bruger.ID;
                Session.Timeout = 120;
                return View("Index", Redigeresiden.Get(1));
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

        //--------------------------------Login slut--------------------------------//

        //--------------------------------Bruger start--------------------------------//

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

        public ActionResult Brugerliste()
        {
            BrugerFac BF = new BrugerFac();

            return View(BF.GetAll());
        }

        //--------------------------------Bruger slut--------------------------------//

        //--------------------------------Forside start--------------------------------//

        public ActionResult ForsideRediger()
        {
            GkkRedigerFac RedigerSide = new GkkRedigerFac();

            GkkBestyrelsenFac bestyrelsenFac = new GkkBestyrelsenFac();

            List<GkkBestyrelsen> bestyrelsesMedlemmer = bestyrelsenFac.GetAll();
            ViewBag.BestyrelsesMedlemmer = bestyrelsesMedlemmer;

            return View(RedigerSide.Get(1));
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ForsideRediger(GkkRediger RedigerForside)
        {
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            RedigerSide.Update(RedigerForside);
            return View(RedigerSide.Get(1));
        }

        [HttpPost]
        public ActionResult ForsideRedigerBestyrelsen(GkkBestyrelsen RedigereBestyrelsen, HttpPostedFileBase file)
        {
            Uploader uploader = new Uploader();

            if (file.ContentLength > 0 && file != null)
            {
                RedigereBestyrelsen.Billede = uploader.UploadImage(file, @"Content\Images\", 250, true);
            }

            GkkBestyrelsenFac bestyrelsenFac = new GkkBestyrelsenFac();
            bestyrelsenFac.Insert(RedigereBestyrelsen);

            //GkkRedigerFac RedigerSide = new GkkRedigerFac();

            return RedirectToAction("ForsideRediger");
        }

        public ActionResult DeleteBestyrelseMedlem(int id)
        {
            GkkBestyrelsenFac bestyrelsenFac = new GkkBestyrelsenFac();

            bestyrelsenFac.Delete(id);

            return RedirectToAction("ForsideRediger");
        }

        //--------------------------------Forside slut--------------------------------//

        //--------------------------------Vedtægter start--------------------------------//

        public ActionResult VedtægterRedigere()
        {
            GkkRedigerFac redigerFacVedtægter = new GkkRedigerFac();

            return View(redigerFacVedtægter.Get(3));
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult VedtægterRedigere(GkkRediger RedigerVedtægter)
        {
            GkkRedigerFac redigerFacVedtægter = new GkkRedigerFac();
            redigerFacVedtægter.Update(RedigerVedtægter);
            return View(redigerFacVedtægter.Get(3));
        }

        //--------------------------------Vedtægter slut--------------------------------//

        //--------------------------------Galleri start--------------------------------//

        public ActionResult GalleriRedigere()
        {
            GkkGalleriFac galleriFac = new GkkGalleriFac();

            List<GkkGalleri> GalleriSamling = galleriFac.GetAll();
            GalleriSamling.Sort((s1, s2) => s2.ID.CompareTo(s1.ID));

            ViewBag.GalleriSamling = GalleriSamling;

            return View();
        }
        
        [HttpPost]
        public ActionResult GalleriRedigere(HttpPostedFileBase file)
        {
            Uploader uploader = new Uploader();
            GkkGalleri RedigereGalleri = new GkkGalleri();
            GkkGalleriFac RedigereGalleriFac = new GkkGalleriFac();

            if (file.ContentLength > 0 && file != null)
            {
                RedigereGalleri.BilledeStor = uploader.UploadImage(file, @"Content\Images\", 0, true);
                RedigereGalleri.BilledeLille = uploader.UploadImage(file, @"Content\Images\", 200, true);

                RedigereGalleriFac.Insert(RedigereGalleri);
            }

            return RedirectToAction("GalleriRedigere");
        }
        
        public ActionResult DeleteGalleriBillede(int id)
        {

            GkkGalleriFac galleriFac = new GkkGalleriFac();

            galleriFac.Delete(id);

            return RedirectToAction("GalleriRedigere");
        }

        //--------------------------------Galleri slut--------------------------------//
    }




}





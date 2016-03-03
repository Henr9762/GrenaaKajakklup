using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using RepoGKK.Factories;
using RepoGKK.Models.BaseModels;


namespace GrenaaKajakklup.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Session["UserLogin"] == null && Request.RawUrl != "/Admin/Admin/UserLogin")
            {
                Response.Redirect("/Home/Index");
                return;
            }
            base.OnActionExecuted(filterContext);
        }

        private Uploader u = new Uploader();
        private GkkSlidderFac Slider = new GkkSlidderFac();

        // GET: Admin/Admin
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
            GkkRedigerFac Redigerside = new GkkRedigerFac();
            Gkkbruger Bruger = new Gkkbruger();
            BrugerFac BF = new BrugerFac();

            Bruger = BF.Login(Name, Crypto.Hash(Password));

            if (Bruger.ID > 0)
            {
                Session["UserLogin"] = Bruger.ID;
                Session.Timeout = 120;
                return View("Index", Redigerside.Get(1));
            }
            else
            {
                ViewBag.MSG = " Brugern blev ikke fundet!";
            }

            return View("UserLogin");
        }

        public ActionResult UserLogin()
        {
            return View();
        }

        //--------------------------------Login slut--------------------------------//

        //--------------------------------Bruger start--------------------------------//

        //public ActionResult OpretBruger()
        //{
        //    return View();
        //}

        [HttpPost]
        public ActionResult OpretBruger(String Name, String Password)
        {
            OpretFac Kf = new OpretFac();
            Gkkbruger K = new Gkkbruger();

            if (Kf.UserExists(Name) != true)
            {
                if (Name.Length != 0 && Password.Length != 0)
                {
                    K.Name = Name;
                    K.Password = Crypto.Hash(Password);
                    Kf.Insert(K);
                    ViewBag.MSG = " Brugern er blevet  oprettet";
                }
                else
                {
                    ViewBag.MSG = " Felterne skal udfyldes";
                }
            }
            else
            {
                ViewBag.MSG = " Fejl brugeren eksiterer allerede";
            }

            return View("Brugerliste", Kf.GetAll());
        }

        public ActionResult SletBruger(int id)
        {
            int BrugerID = int.Parse(Session["UserLogin"].ToString());

            OpretFac Kf = new OpretFac();
            if (BrugerID == id)
            {

            }
            else
            {
                if (id != 49)
                {
            Kf.Delete(id);
                }
                else
                {
                    ViewBag.MSG = "Denne Bruger kan ikke slættes af sikkerheds mæssige grunde";
                }
            }

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
            GkkSlidderFac Redigerslidder = new GkkSlidderFac();
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
                string appPath = Request.PhysicalApplicationPath;
                RedigereBestyrelsen.Billede = uploader.UploadImage(file, appPath + @"Content\Images\", 250, true);
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
        public ActionResult GalleriRedigere(List<HttpPostedFileBase> file)
        {
            Uploader uploader = new Uploader();
            GkkGalleri RedigereGalleri = new GkkGalleri();
            GkkGalleriFac RedigereGalleriFac = new GkkGalleriFac();

            foreach (HttpPostedFileBase fil in file)
            {
                if (fil.ContentLength > 0 && file != null)
            {
                    string appPath = Request.PhysicalApplicationPath;
                    RedigereGalleri.BilledeStor = uploader.UploadImage(fil, appPath + @"Content\Images\", 0, true);
                    RedigereGalleri.BilledeLille = uploader.UploadImage(fil, appPath + @"Content\Images\", 200, true);

                RedigereGalleriFac.Insert(RedigereGalleri);
            }
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

        //--------------------------------Slider start--------------------------------//

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

        //--------------------------------Slider slut--------------------------------//

        //--------------------------------Ro med OS start--------------------------------//

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

        //--------------------------------Ro med OS slut--------------------------------//

        //--------------------------------NyeBegivenheder start--------------------------------//

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

        //--------------------------------NyeBegivenheder start--------------------------------//

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

        //--------------------------------NyeBegivenheder slut--------------------------------//

        //--------------------------------Begivenheder start--------------------------------//

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

        public ActionResult NyeBegivenhederRediger()
        {
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            return View(RedigerSide.Get(10));
        }

        //--------------------------------Begivenheder slut--------------------------------//

        //--------------------------------Tider og Priser start--------------------------------//

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

        //--------------------------------Tider og Priser slut--------------------------------//

        //--------------------------------Logud start--------------------------------//

        public ActionResult Logud()
        {
            Session.Remove("UserLogin");

            return Redirect("/Home/Index");
        }

        //--------------------------------Logud slut--------------------------------//

        //--------------------------------Galleri slut--------------------------------//



        //--------------------------------Kalender start--------------------------------//

        public ActionResult KalenderRediger()
        {
            return View();
        }

        //--------------------------------Kalender slut--------------------------------//



        //--------------------------------Tilmelding start--------------------------------//

        public ActionResult TilmeldingRediger()
        {
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            return View(RedigerSide.Get(12));
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TilmeldingRediger(GkkRediger RedigerTilmeldning)
        {
            RedigerTilmeldning.Overskrift = " ";
            GkkRedigerFac RedigerSide = new GkkRedigerFac();
            RedigerSide.Update(RedigerTilmeldning);
            return View(RedigerSide.Get(12));
        }
        //--------------------------------Tilmelding slut--------------------------------//

    }


}



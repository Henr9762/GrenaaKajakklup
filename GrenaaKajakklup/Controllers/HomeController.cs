using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepoGKK.Factories;
using RepoGKK.Models;
using RepoGKK.Models.BaseModels;

namespace GrenaaKajakklup.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            //TJEK
            GkkSlidderFac sliderFac = new GkkSlidderFac();

            GkkSlidder slider_billede1 = sliderFac.Get(1);
            ViewBag.Slider_Billede1 = slider_billede1.Billedenavn;
            GkkSlidder slider_billede2 = sliderFac.Get(2);
            ViewBag.Slider_Billede2 = slider_billede2.Billedenavn;
            GkkSlidder slider_billede3 = sliderFac.Get(3);
            ViewBag.Slider_Billede3 = slider_billede3.Billedenavn;

            
            GkkRedigerFac test = new GkkRedigerFac();

            GkkBestyrelsenFac bestyrelsenFac = new GkkBestyrelsenFac();

            List<GkkBestyrelsen> bestyrelsesMedlemmer = bestyrelsenFac.GetAll();
            ViewBag.BestyrelsesMedlemmer = bestyrelsesMedlemmer;

            return View(test.Get(1));
        }

        
        public ActionResult RoMedOs()
        {
            GkkRedigerFac redigerFacRoMedOS = new GkkRedigerFac();

            return View(redigerFacRoMedOS.Get(5));
        }

        public ActionResult VinterRoning()
        {
            GkkRedigerFac RedigerVinterroning = new GkkRedigerFac();


            return View(RedigerVinterroning.Get(6));
        }

        public ActionResult TiderOgPriser()
        {
            GkkRedigerFac TiderogPriser = new GkkRedigerFac();


            return View(TiderogPriser.Get(7));
        }

        public ActionResult Vedtægter()
        {
            GkkRedigerFac redigerFacVedtægter = new GkkRedigerFac();

            return View(redigerFacVedtægter.Get(3));
        }

        public ActionResult Begivenheder()
        {
            GkkRedigerFac Begivenheder = new GkkRedigerFac();


            return View(Begivenheder.Get(9));
        }

        public ActionResult KlubAften()
        {
            GkkRedigerFac Klubaften = new GkkRedigerFac();


            return View(Klubaften.Get(8));
        }

        public ActionResult NyeBegivenheder()
        {
            GkkRedigerFac NyeBegivenheder = new GkkRedigerFac();


            return View(NyeBegivenheder.Get(10));
            
        }


        //--------------------------------Galleri Start--------------------------------//
        public ActionResult Galleri()
        {
            GkkGalleriFac galleriFac = new GkkGalleriFac();

            List<GkkGalleri> GalleriSamling = galleriFac.GetAll();
            GalleriSamling.Sort((s1, s2) => s2.ID.CompareTo(s1.ID));

            ViewBag.GalleriSamling = GalleriSamling;

            return View();
        }
        //--------------------------------Galleri Slut--------------------------------//

        public ActionResult Kalender()
        {
            return View();
        }

        public ActionResult Tilmelding()
        {
            GkkRedigerFac Tilmelding = new GkkRedigerFac();

            return View(Tilmelding.Get(12));

        }

        
    }
}
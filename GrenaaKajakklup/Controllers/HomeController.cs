using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepoGKK.Factories;
using RepoGKK.Models.BaseModels;

namespace GrenaaKajakklup.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            GkkRedigerFac test = new GkkRedigerFac();

            GkkBestyrelsenFac bestyrelsenFac = new GkkBestyrelsenFac();

            List<GkkBestyrelsen> bestyrelsesMedlemmer = bestyrelsenFac.GetAll();
            ViewBag.BestyrelsesMedlemmer = bestyrelsesMedlemmer;

            return View(test.Get(1));
        }

        
        public ActionResult RoMedOs()
        {
            return View();
        }

        public ActionResult VinterRoning()
        {
            return View();
        }

        public ActionResult TiderOgPriser()
        {
            return View();
        }

        public ActionResult Vedtægter()
        {
            GkkRedigerFac redigerFacVedtægter = new GkkRedigerFac();

            return View(redigerFacVedtægter.Get(3));
        }

        public ActionResult Begivenheder()
        {
            return View();
        }

        public ActionResult KlubAften()
        {
            return View();
        }

        public ActionResult NyeBegivenheder()
        {
            return View();
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
            return View();
        }

        
    }
}
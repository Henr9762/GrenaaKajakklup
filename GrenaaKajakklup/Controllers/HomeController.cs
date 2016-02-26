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


            return View();
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
            return View();
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

        public ActionResult Galleri()
        {
            return View();
        }

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
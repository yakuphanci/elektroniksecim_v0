using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using elektroniksecim_v0.Models.Entity;
namespace elektroniksecim_v0.Controllers
{
    public class ProfilController : Controller
    {

        
        elektroniksecimEntities db = new elektroniksecimEntities();
        // GET: Profil,


        [Authorize]
        public ActionResult Index()
        {
            if (Session["kullaniciID"] == null)
                return RedirectToAction("Index", "Login");
            var secmenID = Convert.ToInt64(Session["secmenID"]);
            var aktifsecimler = db.SecmeninOyKullanabilecegiSecimler(secmenID).ToList();
            return View(aktifsecimler);
        }

      
    }
}
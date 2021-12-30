using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using elektroniksecim_v0.Models.Entity;

namespace elektroniksecim_v0.Controllers
{
    public class VoteController : Controller
    {
        elektroniksecimEntities db = new elektroniksecimEntities();
        // GET: Vote

     
     
        [Authorize]
        public ActionResult Index(long secimID)
        {
            var secmenID = Convert.ToInt64(Session["secmenID"]);
            if (DbFunctions.OyHakkiVarMi(secmenID, secimID))
            {
                ViewBag.secimID = secimID;
                var secimAdayBilgiler =
                       db.Aday.Where(a => a.secimID == secimID).ToList();
                ViewBag.secimID = secimID;
                return View(secimAdayBilgiler);
            }
            else
            {
                //Bu seçim için oy kullanma hakkı yoksa profile geri yönlendir.
                return RedirectToAction("Index", "Profil");
            }
        }
   
        [Authorize]
        public ActionResult OyKullan(long secimID, long adayID)
        {
            try
            {
                var secmenID = Convert.ToInt64(Session["secmenID"]);
                if (DbFunctions.OyHakkiVarMi(secmenID, secimID))
                {
                    db.OyKullan(secimID, secmenID, adayID);
                    TempData["OyKullanildi"] = true;
                }
                else
                    TempData["Error"] = "Bu seçim için oy kullanma hakkınız bulunmuyor.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index", "Profil", new { secimID });
        }
    }
}
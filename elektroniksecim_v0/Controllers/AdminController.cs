using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using elektroniksecim_v0.Models.Entity;
namespace elektroniksecim_v0.Controllers
{
    public class AdminController : Controller
    {
        elektroniksecimEntities db = new elektroniksecimEntities();

        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            if (Session["adminID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Profil");
            }
        }

        [Authorize]
        public ActionResult GelecekSecimler()
        {
            var baslamamisSecimler = db.GetBaslamamisSecimler.ToList();
            return View(baslamamisSecimler);
        }

        [Authorize]
        public ActionResult SecimIptal(long secimID)
        {
            if (Session["adminID"] != null)
            {
                db.SecimIptalOlarakIsaretle(secimID);
                TempData["SecimSilindi"] = "\'" + secimID + "\' Numaralı Seçim İptal Edildi ";
            }
            return RedirectToAction("GelecekSecimler");
        }

        [Authorize]
        public ActionResult IptalEdilenSecimler()
        {
            var iptalEdilmisSecimler = db.GetIptalEdilmisSecimler.ToList();
            return View(iptalEdilmisSecimler);
        }

        [Authorize]
        public ActionResult BaslamisSecimler()
        {
            var baslamisSecimler = db.GetBaslamisSecimler.ToList();
            return View(baslamisSecimler);
        }

        [Authorize]
        public ActionResult BitmisSecimler()
        {

            var bitmisSecimler = db.GetBitmisSecimler.ToList();

            return View(bitmisSecimler);
        }

        [Authorize]
        public ActionResult YeniSecim()
        {
            if (Session["adminID"] == null)
                return RedirectToAction("Index", "Login");
            return View();
        }

        [Authorize]
        public ActionResult SecimOlustur(string secimAdi, string baslangicTarihi, string baslangicSaati, string bitisTarihi, string bitisSaati)
        {
            try
            {
                var baslangicZamani = DateTimeOffset.Parse(baslangicTarihi.Replace('-', '.') + " " + baslangicSaati);
                var bitisZamani = DateTimeOffset.Parse(bitisTarihi.Replace('-', '.') + " " + bitisSaati);

                db.SecimOlustur(secimAdi, baslangicZamani, bitisZamani);
                TempData["SecimOlustu"] = "\'" + secimAdi + "\' isimli seçim başarılı bir şekilde oluşturuldu.";

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }


            return RedirectToAction("YeniSecim");
        }

        [Authorize]
        [HttpGet]
        public ActionResult YeniAday(long secimID)
        {
            ViewBag.secimID = secimID;
            ViewBag.secimAdi = db.Secim.Where(s => s.secimID == secimID).ToList().FirstOrDefault().secimAdi;

            var adaylar = db.Aday.Where(a => a.secimID == secimID).ToList();

            return View(adaylar);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AdayEkle(string adayAdi, long secimID)
        {
            try
            {
                if (Session["adminID"] == null)
                    return RedirectToAction("LogOut", "Login");
                db.AdayEkle(secimID, adayAdi);
                TempData["AdayEklendi"] = "\'" + adayAdi + "\' isimli aday başarılı bir şekilde eklendi.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Aday eklenirken bir hata oluştu. " + Environment.NewLine + ex.Message;
            }
           
            return Redirect("/Admin/YeniAday?secimID="+secimID);
        }

        [Authorize]
        public ActionResult AdaySil(long adayID, long secimID, string adayAdi)
        {
            if(Session["adminID"] == null)
                return RedirectToAction("LogOut", "Login");

            db.AdaySil(secimID, adayID);
            TempData["AdaySilindi"] = "\'" + adayAdi + "\' isimli aday başarılı bir şekilde seçimden çıkarıldı.";
            return Redirect("/Admin/YeniAday?secimID=" + secimID);
        }

        [Authorize]
        [HttpGet]
        public ActionResult SecmenAta(long secimID)
        {
            var secim = db.GetBaslamamisSecimler.Where(s => s.secimID == secimID).ToList().FirstOrDefault();
            if (secim != null)
            {
                ViewBag.secimID = secimID;
                ViewBag.secimAdi = DbFunctions.GetSecimAdi(secimID);
                var secmenler = db.SecmeHakki.Where(s => s.secimID == secimID).ToList();
                return View(secmenler);
            }
            else
            {
                return Redirect("/Admin/Index");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult TumSecmenleriAta(long secimID)
        {
            try
            {
                ViewBag.secimID = secimID;
                ViewBag.secimAdi = DbFunctions.GetSecimAdi(secimID);
                db.TumSecmenlereSecimHakkiTanimla(secimID);
                TempData["SecmenAtandi"] = "Bu seçim için tüm seçmenlere oy hakkı başarılı bir şekilde atandı.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return Redirect("/Admin/SecmenAta?secimID=" + secimID);
        }


    }
}
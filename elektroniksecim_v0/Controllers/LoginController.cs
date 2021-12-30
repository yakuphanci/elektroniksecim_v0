using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using elektroniksecim_v0.Models.Entity;
namespace elektroniksecim_v0.Controllers
{
    public class LoginController : Controller
    {

        elektroniksecimEntities db = new elektroniksecimEntities();
        // GET: Login

        [HttpGet]
        public ActionResult Index()
        {
            //Eğer zaten kullanıcı giriş yaptıysa giriş ekranı yerine otomatik profil ekranına yolla.
            if (Session["kullaniciID"] != null)
                return RedirectToAction("Index", "Profil");
            
            //Login işlemlerinde bir anormallik varsa her şeyi temizle. 
            return RedirectToAction("LogOut");
        }
        
        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            //Giriş yapmaya çalışan kullanıcının bilgilerini doğrula. Doğruysa bilgileri çek.
            var validKullanici =
                       db.Kullanici.Where(k => k.kullaniciAdi == kullanici.kullaniciAdi && k.parola == kullanici.parola).FirstOrDefault();

            if (validKullanici != null)
            {
                //authentication işlemleri.
                FormsAuthentication.SetAuthCookie(validKullanici.kullaniciAdi, false);

                //yetkilendirme tamam lazım olabilecek bilgileri sessionlara atıyoruz.
                Session["kullaniciID"] = validKullanici.kullaniciID;
                Session["kullaniciAdi"] = validKullanici.kullaniciAdi;
                Session["kullaniciFullName"] = validKullanici.adi +" " +validKullanici.soyadi;


                //Adminliği varsa admin bilgilerini çek
                var validAdmin =
                    db.Admin.Where(a => a.kullaniciID == validKullanici.kullaniciID).FirstOrDefault();
                if (validAdmin != null)
                {
                    Session["adminID"] = validAdmin.adminID;
                }

                //Seçmenliği varsa seçmen bilgilerini çek.
                var validSecmen =
                    db.Secmen.Where(s => s.kullaniciID == validKullanici.kullaniciID).FirstOrDefault();
                if (validSecmen != null)
                {
                    Session["secmenID"] = validSecmen.secmenID;
                }

                //Giriş yapıldığı için profile yönlendir.
                return RedirectToAction("Index", "Profil");
            }  
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult LogOut()
        {
            //Çıkış yapma işlemleri.
            Session.Clear();
            FormsAuthentication.SignOut();
            return View("Index");
        }
        
    }
}
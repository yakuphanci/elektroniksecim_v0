using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using elektroniksecim_v0.Models.Entity;
namespace elektroniksecim_v0
{
    public class DbFunctions
    {
        public static bool OyHakkiVarMi(long secmenID, long secimID)
        {
            using (elektroniksecimEntities db = new elektroniksecimEntities())
            {
                return db.Database.SqlQuery<bool>("select dbo.fn_OyHakkiVarMi(@p0,@p1)", secmenID, secimID).FirstOrDefault();
            }
        }

        public static string GetSecmenAdi(long secmenID)
        {
            using (elektroniksecimEntities db = new elektroniksecimEntities())
            {
                var secmenKullaniciID = db.Secmen.Where(s => s.secmenID == secmenID).ToList().FirstOrDefault().kullaniciID;
                var kullanici = db.Kullanici.Where(k => k.kullaniciID == secmenKullaniciID).ToList().FirstOrDefault();
                return kullanici.adi + " " + kullanici.soyadi;
            }
        }

        public static string GetSecimAdi(long secimID)
        {
            using (elektroniksecimEntities db = new elektroniksecimEntities())
            {
                var secimAdi = db.Secim.Where(s => s.secimID == secimID).ToList().FirstOrDefault().secimAdi;

                return secimAdi;
            }
        }

        public static string ConvertHashToString(byte[] hash)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in hash)
                stringBuilder.AppendFormat("{0:X2}", b);

            return stringBuilder.ToString();
        }
    }
}
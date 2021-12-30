using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using elektroniksecim_v0.Models.Entity;
namespace elektroniksecim_v0.Controllers
{
    public class BlockChainController : Controller
    {
        elektroniksecimEntities db = new elektroniksecimEntities();
        // GET: BlockChain
        [Authorize]
        public ActionResult Index()
        {
            if (Session["adminID"] == null)
                return RedirectToAction("LogOut", "Login");
            var chain = db.ChainWithValid.ToList();
            return View(chain);
        }
    }
}
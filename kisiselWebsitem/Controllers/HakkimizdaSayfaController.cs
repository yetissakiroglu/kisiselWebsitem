using kisiselWebsitem.Models.DataContext;
using kisiselWebsitem.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace kisiselWebsitem.Controllers
{
    public class HakkimizdaSayfaController : Controller
    {
        KisiselWebDBContext db = new KisiselWebDBContext();
        // GET: Hakkimizda
        public ActionResult Index()
        {
            var h = db.Hakkimizda.ToList();
            return View(h);
        }
        public ActionResult Edit(int id)
        {
            var h = db.Hakkimizda.Where(x => x.HakkimizdaId == id).FirstOrDefault();

            return View(h);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Hakkimizda h, HttpPostedFileBase imgURL)
        {
            if (ModelState.IsValid)
            {
                var hakkimizda = db.Hakkimizda.Where(x => x.HakkimizdaId == id).SingleOrDefault();

                if (imgURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(hakkimizda.imgURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(hakkimizda.imgURL));
                    }
                    WebImage img = new WebImage(imgURL.InputStream);
                    FileInfo imginfo = new FileInfo(imgURL.FileName);

                    string logoname = imgURL.FileName + imginfo.Extension;
                    img.Save("~/Uploads/Hakkimizda/" + logoname);

                    hakkimizda.imgURL = "/Uploads/Hakkimizda/" + logoname;
                }

                hakkimizda.Baslik = h.Baslik;
                hakkimizda.Keywords = h.Keywords;
                hakkimizda.Description = h.Description;

                hakkimizda.Aciklama = h.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(h);
        }
    }
}
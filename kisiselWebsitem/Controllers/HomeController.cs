using kisiselWebsitem.Models.DataContext;
using kisiselWebsitem.Models.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace kisiselWebsitem.Controllers
{
    public class HomeController : Controller
    {
        private KisiselWebDBContext db = new KisiselWebDBContext();
        // GET: Home
        [Route("")]
        [Route("Anasayfa")]

        public ActionResult Index()
        {
            Ayarlar ayarlar = new Ayarlar();
            ayarlar = db.Ayarlar.SingleOrDefault();

            if (ayarlar == null)
            {
                ViewBag.Ayarlar = ayarlar;
            }
            else
            {
                ViewBag.Ayarlar = db.Ayarlar.SingleOrDefault();
            }

            ViewBag.Ayarlar = db.Ayarlar.SingleOrDefault();
            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x => x.HizmetId);
            ViewBag.Bloglar = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Menu = db.Menu.ToList().OrderBy(x => x.Siralama);
            return View();
        }

        public ActionResult SliderPartial()
        {
            return View(db.Slider.ToList().OrderByDescending(x => x.SliderId));
        }

        public ActionResult HizmetPartial()
        {
            return View(db.Hizmet.ToList());
        }
        [Route("Hakkimizda")]
        public ActionResult Hakkimizda()
        {

            ViewBag.Ayarlar = db.Ayarlar.SingleOrDefault();
            return View(db.Hakkimizda.SingleOrDefault());
        }
       

        [Route("iletisim")]
        public ActionResult Iletisim()
        {
            ViewBag.Ayarlar = db.Ayarlar.SingleOrDefault();
            return View(db.Iletisim.SingleOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Iletisim(string adsoyad = null, string email = null, string konu = null, string mesaj = null)
        {

            if (adsoyad != null && email != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "kurumsalweb01@gmail.com";
                WebMail.Password = "Kurumsal36987";
                WebMail.SmtpPort = 587;
                WebMail.Send("kurumsalweb01@gmail.com", konu, email + "-" + mesaj);
                ViewBag.Uyari = "Mesajınız başarı ile gönderilmiştir.";
                Response.Redirect("/iletisim");

            }
            else
            {
                ViewBag.Uyari = "Hata Oluştu.Tekrar deneyiniz";
            }
            return View();
        }
        [Route("BlogPost")]
        public ActionResult Blog(int Sayfa = 1)
        {
            ViewBag.Ayarlar = db.Ayarlar.SingleOrDefault();

            return View(db.Blog.Include("Kategori").OrderByDescending(x => x.BlogId).ToPagedList(Sayfa, 5));
        }
        [Route("BlogPost/{kategoriad}/{id:int}")]
        public ActionResult KategoriBlog(int id, int Sayfa = 1)
        {
            ViewBag.Ayarlar = db.Ayarlar.SingleOrDefault();
            var b = db.Blog.Include("Kategori").OrderByDescending(x => x.BlogId).Where(x => x.Kategori.KategoriId == id).ToPagedList(Sayfa, 5);
            return View(b);
        }
        [Route("BlogPost/{baslik}-{id:int}")]
        public ActionResult BlogDetay(int id)
        {
            ViewBag.Ayarlar = db.Ayarlar.SingleOrDefault();
            var b = db.Blog.Include("Kategori").Include("Yorums").Where(x => x.BlogId == id).SingleOrDefault();
            return View(b);
        }

        public JsonResult YorumYap(string adsoyad, string eposta, string icerik, int blogid)
        {
            if (icerik == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorum.Add(new Yorum { AdSoyad = adsoyad, Eposta = eposta, Icerik = icerik, BlogId = blogid, Onay = false });
            db.SaveChanges();

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BlogKategoriPartial()
        {
            ViewBag.Ayarlar = db.Ayarlar.SingleOrDefault();

            return PartialView(db.Kategori.Include("Blogs").ToList().OrderBy(x => x.KategoriAd));
        }
        public ActionResult BlogKayitPartial()
        {

            return PartialView(db.Blog.ToList().OrderByDescending(x => x.BlogId));
        }

        public ActionResult FooterPartial()
        {
            ViewBag.Ayarlar = db.Ayarlar.SingleOrDefault();

            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x => x.HizmetId);

            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();

            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);
            ViewBag.Menu = db.Menu.ToList().OrderBy(x => x.Siralama);

            return PartialView();
        }


    }
}
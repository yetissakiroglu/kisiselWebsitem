using kisiselWebsitem.Models.DataContext;
using kisiselWebsitem.Models.Model;
using kisiselWebsitem.Models.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            ViewBag.Menu = db.Menu.ToList().OrderBy(x => x.Siralama);
            ViewBag.Ayarlar = db.Ayarlar.SingleOrDefault();
            return View(db.Hakkimizda.SingleOrDefault());
        }
       

        [Route("iletisim")]
        public ActionResult Iletisim()
        {
            ViewBag.Menu = db.Menu.ToList().OrderBy(x => x.Siralama);
            ViewBag.Ayarlar = db.Ayarlar.SingleOrDefault();
            return View(db.Iletisim.SingleOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Iletisim(string adsoyad = null, string email = null, string konu = null, string mesaj = null)
        {

            if (adsoyad != null && email != null)
            {
         
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("arifkaplan2017@yandex.com");
                mail.To.Add("arifkaplan2017@yandex.com");
                mail.Subject = konu;
                mail.Body = mesaj;
                mail.BodyEncoding = System.Text.Encoding.GetEncoding(1254);
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.Host = "smtp.yandex.ru";
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("arifkaplan2017@yandex.com", "20016565arif");
                smtp.Send(mail);


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
        public ActionResult MenuPartial()
        {
            ViewBag.Ayarlar = db.Ayarlar.SingleOrDefault();

            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x => x.HizmetId);

            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();

            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);

            HomeViewModel homeView = new HomeViewModel();
            homeView.Menuler = db.Menu.ToList().OrderBy(x => x.Siralama).ToList();


            return PartialView(homeView);
        }
        public ActionResult FooterPartial()
        {
            ViewBag.Ayarlar = db.Ayarlar.SingleOrDefault();

            ViewBag.Hizmetler = db.Hizmet.ToList().OrderByDescending(x => x.HizmetId);

            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();

            ViewBag.Blog = db.Blog.ToList().OrderByDescending(x => x.BlogId);

            HomeViewModel homeView = new HomeViewModel();
            homeView.Menuler = db.Menu.ToList().OrderBy(x => x.Siralama).ToList();


            return PartialView(homeView);
        }


    }
}
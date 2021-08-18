using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kisiselWebsitem.Models.Model
{
    [Table("Menu")]

    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        [DisplayName("Menü Başlık")]
        public string MenuAdi { get; set; }
        [DisplayName("Menü Url")]
        public string Url { get; set; }
        [DisplayName("Sıralama")]
        public int Siralama { get; set; }

    }
}
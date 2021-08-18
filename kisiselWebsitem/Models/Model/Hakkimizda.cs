using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kisiselWebsitem.Models.Model
{
    [Table("Hakkimizda")]
    public class Hakkimizda
    {
        [Key]
        public int HakkimizdaId { get; set; }
        [DisplayName("Başlık")]
        [Required, StringLength(500, ErrorMessage = "500 Karekter olmalıdır")]
        public string Baslik { get; set; }
        [DisplayName("Anahtar Kelimeler")]
        [Required, StringLength(250, ErrorMessage = "250 Karekter olmalıdır")]
        public string Keywords { get; set; }
        [DisplayName("Site Açıklama")]
        [Required, StringLength(500, ErrorMessage = "500 Karekter olmalıdır")]
        public string Description { get; set; }
        [DisplayName("Kapak Resmi")]
        public string imgURL { get; set; }
        [Required]
        [DisplayName("Hakkımızda Açıklama")]
        public string Aciklama { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SzkolkaSkierniewice.Domain.Entities
{
    public class Promotion
    {
        [HiddenInput(DisplayValue = false)]
        public int PromotionID { get; set; }
        [Required(ErrorMessage = "Proszę uzupełnić")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data")]
        public DateTime Date { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data zakończenia")]
        public DateTime EndDate { get; set; }
        [DataType(DataType.MultilineText), Display(Name = "Opis")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Proszę uzupełnić")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Proszę podać dodatnią wartość")]
        [Display(Name = "Cena (zł)")]
        public decimal Price { get; set; }
        [Display(Name = "Link do promocji")]
        public string Link { get; set; }
        [Display(Name = "Promocja zawiera darmowy projekt ogrodu")]
        public bool FreeGardenProject { get; set; }

        public virtual ICollection<ProductInPromotion> ProductPromotionList { get; set; }
    }
}
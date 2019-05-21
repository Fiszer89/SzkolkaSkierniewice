using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SzkolkaSkierniewice.Domain.Entities
{
    public enum Category
    {
        IglakiGrunt, IglakiPojemnik, DrzewaAlejoweGrunt, KrzewyLiściastePojemnik
    }

    public abstract class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Proszę uzupełnić")]
        [Display(Name = "Dostępność")]
        public bool Available { get; set; }
        [Required(ErrorMessage = "Proszę uzupełnić")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Proszę uzupełnić")]
        [DataType(DataType.Date, ErrorMessage="Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data dodania")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Proszę uzupełnić")]
        [Display(Name = "Wielkość minimalna (metry)")]
        [Range(1, double.MaxValue, ErrorMessage = "Proszę podać dodatnią wartość")]
        public double HeightMin { get; set; }
        [Display(Name = "Wielkość maksymalna (metry)")]
        [Range(1, double.MaxValue, ErrorMessage = "Proszę podać dodatnią wartość")]
        public double? HeightMax { get; set; }
        [Display(Name = "Ilość (sztuki)")]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę podać dodatnią wartość")]
        public int? Quantity { get; set; }
        [DataType(DataType.MultilineText), Display(Name = "Opis")]
        public string Description { get; set; }
        /*[Required(ErrorMessage = "Proszę uzupełnić")]
        [Display(Name = "Kategoria")]
        public Category Category { get; set; }*/
        [Required(ErrorMessage = "Proszę uzupełnić")]
        [Range(0.01, double.MaxValue, ErrorMessage="Proszę podać dodatnią wartość")]
        [Display(Name = "Cena (zł)")]
        public decimal Price { get; set; }
        [Display(Name = "Zniżka procentowa")]
        [Range(0.01, 1, ErrorMessage="Proszę podać wartość od 0.01 do 1")]
        public decimal? Discount { get; set; }
        [Display(Name = "Cena po zniżce (zł)")]
        public decimal PriceAfterDicount { get{ return Discount.HasValue ? Price - (Price * (decimal)Discount) : Price; } }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public virtual ICollection<ProductInPromotion> ProductPromotionList { get; set; }
    }
}
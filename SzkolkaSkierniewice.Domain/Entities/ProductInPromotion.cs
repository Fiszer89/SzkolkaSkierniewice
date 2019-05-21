using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SzkolkaSkierniewice.Domain.Entities
{
    public class ProductInPromotion
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductInPromotionID { get; set; }
        public int ProductID { get; set; }
        public int PromotionID { get; set; }
        [Required(ErrorMessage = "Proszę uzupełnić")]
        [Display(Name = "Ilość")]
        public int QuantityOfProductsInPromotion { get; set; }

        public virtual Product Product { get; set; }
        public virtual Promotion Promotion { get; set; }
    }
}
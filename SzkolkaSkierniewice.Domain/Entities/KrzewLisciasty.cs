using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SzkolkaSkierniewice.Domain.Entities
{
    public class KrzewLisciasty:Product
    {
        //public virtual ICollection<ProductInPromotion> ProductPromotionList { get; set; }
        public virtual ICollection<Box> Boxes { get; set; }
    }
}
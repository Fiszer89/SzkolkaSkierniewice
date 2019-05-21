using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SzkolkaSkierniewice.Domain.Entities
{
    public class DrzewoAlejowe:Product
    {
        [Required(ErrorMessage = "Proszę uzupełnić")]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę podać dodatnią wartość")]
        [Display(Name = "Obwód pnia min (cm)")]
        public int WidthMin { get; set; }
        [Display(Name = "Obwód pnia max (cm)")]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę podać dodatnią wartość")]
        public int? WidthMax { get; set; }
    }
}
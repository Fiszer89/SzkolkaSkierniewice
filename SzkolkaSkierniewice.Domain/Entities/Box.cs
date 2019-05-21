using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace SzkolkaSkierniewice.Domain.Entities
{
    public class Box
    {
        [HiddenInput(DisplayValue = false)]
        public int BoxID { get; set; }
        [Required(ErrorMessage = "Proszę uzupełnić")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        public virtual ICollection<IglakPojemnik> IglakPojemnik { get; set; }
        public virtual ICollection<KrzewLisciasty> KrzewLisciastyPojemnik { get; set; }
    }
}
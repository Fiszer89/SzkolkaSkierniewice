using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SzkolkaSkierniewice.Domain.Entities
{
    public class IglakPojemnik:Product
    {
        public virtual ICollection<Box> Boxes { get; set; }
    }
}
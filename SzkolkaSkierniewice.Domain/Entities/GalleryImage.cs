using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SzkolkaSkierniewice.Domain.Entities
{
    public class GalleryImage
    {
        [HiddenInput(DisplayValue = false)]
        public int GalleryImageID { get; set; }
        [Required(ErrorMessage = "Proszę uzupełnić")]
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data dodania")]
        public DateTime Date { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SzkolkaSkierniewice.Models
{
    public class ContactModels
    {
        [Required(ErrorMessage = "Imię jest wymagane")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email jest wymagany")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Komentarz jest wymagany")]
        public string Comment { get; set; }
    }
}
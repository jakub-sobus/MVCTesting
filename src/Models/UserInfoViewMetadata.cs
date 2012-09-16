using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Models;
using System.ComponentModel;

namespace Models
{
    public class UserInfoViewMetadata
    {
        
        public string username { get; set; }

        [Required]
        [DisplayName("Imię")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Nazwisko")]
        public string Surname { get; set; }

        //[Required]
        //[RegularExpression(@"[A-Za-z0-9\._\%\-\+]+@[A-Za-z0-9\.\-]+\.[A-Za-z0-9]{2,4}", ErrorMessage = "Niepoprawny adres Email")]
        //public string Email { get; set; }

        [Required]
        [DisplayName("Nazwa projektu")]
        public string Project { get; set; }

        [Required]
        [DisplayName("Miasto")]
        public string City { get; set; }

        [Required]
        [DisplayName("Ulica")]
        public string Street { get; set; }

        [Required]
        [DisplayName("Numer domu")]
        public string House_number { get; set; }

        [Required]
        [DisplayName("Numer mieszkania")]
        public string Apartment_number { get; set; }

        [Required]
        [DisplayName("Kod pocztowy")]
        [RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "Kod pocztowy niepoprawny")]
        public string Zip_code { get; set; }
    }

}

﻿using System.ComponentModel.DataAnnotations;

namespace MyVet.Web.Data.Entities
{
    public class PlaceSelling
    {
        public int Id { get; set; }

        [Display(Name = "Place Selling ")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }
    }
}

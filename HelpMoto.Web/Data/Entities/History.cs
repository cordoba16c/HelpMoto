﻿using System;
using System.ComponentModel.DataAnnotations;

namespace HelpMoto.Web.Data.Entities
{
    public class History
    {
        public int Id { get; set; }

        [Display(Name = "Description")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Description { get; set; }

        [Display(Name = "InicialDate")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime InicialDate { get; set; }

        [Display(Name = "FinalDate")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime FinalDate { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "InicialDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateLocal => InicialDate.ToLocalTime();

        [Display(Name = "FinalDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime FinalDateLocal => FinalDate.ToLocalTime();

        public WorkshopType WorkshopType { get; set; }

        public Motorcycle motorcycle { get; set; }
    }
}

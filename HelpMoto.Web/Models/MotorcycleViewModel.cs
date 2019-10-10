using HelpMoto.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HelpMoto.Web.Models
{
    public class MotorcycleViewModel:Motorcycle
    {
        public int OwnerId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Motorcycle Type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a motorcycle type.")]
        public int MotorcycleTypeId { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        public IEnumerable<SelectListItem>MotorcycleTypes { get; set; }
    }
}

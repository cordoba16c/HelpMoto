using HelpMoto.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpMoto.Web.Models
{
    public class HistoryViewModel : History
    {
        public int MotorcycleId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Workshop Type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a service type.")]
        public int WorkshopTypeId { get; set; }

        public IEnumerable<SelectListItem> WorkshopTypes { get; set; }
    }
}
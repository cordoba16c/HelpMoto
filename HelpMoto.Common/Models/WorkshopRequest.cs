using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelpMoto.Common.Models
{
    public class WorkshopRequest
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public string ContactName { get; set; }

        public string PhoneName { get; set; }

        public string Remarks { get; set; }

        public int WorkshopTypeId  { get; set; }
    }
}

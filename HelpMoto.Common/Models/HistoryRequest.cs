using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelpMoto.Common.Models
{
   public  class HistoryRequest
    {
        public int Id { get; set; }

        public string Description { get; set; }
        [Required]
        public DateTime InicialDate { get; set; }
        [Required]
        public DateTime FinalDate { get; set; }

        public string Remarks { get; set; }

        public string WorkshopType { get; set; }
    }
}

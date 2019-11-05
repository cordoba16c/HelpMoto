using System;
using System.Collections.Generic;
using System.Text;

namespace HelpMoto.Common.Models
{
    public class HistoryResponse
    {

        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime InicialDate { get; set; }

        public DateTime FinalDate { get; set; }

        public string Remarks { get; set; }

        public string WorkshopType { get; set; }
    }
}

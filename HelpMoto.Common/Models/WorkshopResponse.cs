using System;
using System.Collections.Generic;
using System.Text;

namespace HelpMoto.Common.Models
{
    public class WorkshopResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string ContactName { get; set; }

        public string PhoneName { get; set; }

        public string Remarks { get; set; }

        public string WorkshopType { get; set; }
    }
}

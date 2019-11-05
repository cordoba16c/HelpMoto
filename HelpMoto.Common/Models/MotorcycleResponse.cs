using System;
using System.Collections.Generic;
using System.Text;

namespace HelpMoto.Common.Models
{
    public class MotorcycleResponse
    {
        
            public int Id { get; set; }

            public string Name { get; set; }

            public string ImageUrl { get; set; }

            public string Brand { get; set; }

            public DateTime Shop { get; set; }

            public string Remarks { get; set; }

            public DateTime ShopLocal { get; set; }

            public string MotorcycleType { get; set; }

            public ICollection<HistoryResponse> Histories { get; set; }
    }

}


using System;
using System.ComponentModel.DataAnnotations;

namespace HelpMoto.Common.Models
{
    public class MotorcycleRequest
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Brand { get; set; }

        public int OwnerId { get; set; }

        public int MotorcycleTypeId { get; set; }

        [Required]
        public DateTime Shop { get; set; }

        public string Remarks { get; set; }

        public byte[] ImageArray { get; set; }
    }
}

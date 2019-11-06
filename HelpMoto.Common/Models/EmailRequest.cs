using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HelpMoto.Common.Models
{
    public class EmailRequest
    {
        [Required]
        public string Email { get; set; }
    }
}

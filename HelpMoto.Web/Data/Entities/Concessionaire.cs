using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpMoto.Web.Data.Entities
{
    public class Concessionaire
    {
        public int Id { get; set; }

        [Display(Name = "Concessionaire ")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public Owner Owner { get; set; }
    }
}

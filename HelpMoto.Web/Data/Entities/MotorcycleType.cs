using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpMoto.Web.Data.Entities
{
    public class MotorcycleType
    {
        public int Id { get; set; }

        [Display(Name = "Motorcycle Type")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<Motorcycle> Motorcycles { get; set; }
    }
}

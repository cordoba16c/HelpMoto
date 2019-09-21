using System.ComponentModel.DataAnnotations;

namespace MyVet.Web.Data.Entities
{
    public class WorkshopService
    {
        public int Id { get; set; }

        [Display(Name = "Workshop Service ")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }
    }
}

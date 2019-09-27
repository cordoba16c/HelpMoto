using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpMoto.Web.Data.Entities
{
    public class Motorcycle
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Model")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Model { get; set; }

        [Display(Name = "Shop")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Shop { get; set; }
        public string Remarks { get; set; }


        //TODO: replace the correct URL for the image
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? null
            : $"https://TDB.azurewebsites.net{ImageUrl.Substring(1)}";

        [Display(Name = "Shop")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime ShopLocal => Shop.ToLocalTime();


        public MotorcycleType MotorcycleType { get; set; }
        public Owner Owner { get; set; }
        public ICollection<PlaceSelling> PlaceSellings { get; set; }
        public ICollection<CraneService> CraneServices { get; set; }
        public ICollection<WorkshopService> WorkshopServices { get; set; }
    }
}

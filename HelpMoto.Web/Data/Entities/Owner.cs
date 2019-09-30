using System.Collections.Generic;

namespace HelpMoto.Web.Data.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public User User { get; set; }
        public ICollection<Motorcycle> Motorcycles { get; set; }
        public ICollection<Concessionaire> Concessionaires { get; set; }
        public ICollection<ExtraService> ExtraServices { get; set; }
        public ICollection<PlaceSelling> PlaceSellings { get; set; }
    }
}

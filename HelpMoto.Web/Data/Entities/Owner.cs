using System.Collections.Generic;

namespace HelpMoto.Web.Data.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public User User { get; set; }
        public ICollection<Motorcycle> Motorcycles { get; set; }
    }
}

using HelpMoto.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelpMoto.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<MotorcycleType> MotorcycleTypes { get; set; }
        public DbSet<History> Histories { get; set; }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Manager> Managers { get; set; }

        public DbSet<WorkshopType> WorkshopTypes { get; set; }
    }
}

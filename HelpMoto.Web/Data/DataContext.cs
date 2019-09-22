using HelpMoto.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpMoto.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Concessionaire> Concessionaires  { get; set; }

        public DbSet<CraneService> CraneServices  { get; set; }

        public DbSet<ExtraService> ExtraServices   { get; set; }

        public DbSet<Motorcycle> Motorcycles { get; set; }

        public DbSet<MotorcycleType> MotorcycleTypes { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<PlaceSelling> PlaceSellings { get; set; }

        public DbSet<PlaceSellingType> PlaceSellingTypes { get; set; }

        public DbSet<WorkshopService> WorkshopServices { get; set; }

        public DbSet<WorkshopType> WorkshopTypes { get; set; }
    }
}

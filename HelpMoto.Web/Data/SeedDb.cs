using HelpMoto.Web.Data.Entities;
using HelpMoto.Web.Herlpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HelpMoto.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckMotorcycleTypesAsync();
            await CheckPlaceSellingTypesAsync();
            await CheckOwnersAsync();
            await CheckMotorcyclesAsync();
            await CheckWorkshopServicesAsync();
            await CheckConcessionairesAsync();
            await CheckCraneServicesAsync();
            await CheckPlaceSellingsAsync();
            await CheckWorkshopTypesAsync();
            
        }

        private async Task CheckMotorcyclesAsync()
        {
            var owner = _context.Owners.FirstOrDefault();
            var motorcycleType = _context.MotorcycleTypes.FirstOrDefault();
            if (!_context.Motorcycles.Any())
            {
                AddMotorcycle("CB1", owner, motorcycleType, "Honda");
                AddMotorcycle("BWIS", owner, motorcycleType, "Yamaha");
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckPlaceSellingTypesAsync()
        {
            if (!_context.PlaceSellingTypes.Any())
            {
                _context.PlaceSellingTypes.Add(new PlaceSellingType { Name = "Cascos" });
                _context.PlaceSellingTypes.Add(new PlaceSellingType { Name = "Repuestos" });
                _context.PlaceSellingTypes.Add(new PlaceSellingType { Name = "Llantas" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckWorkshopServicesAsync()
        {
            if (!_context.WorkshopServices.Any())
            {
                _context.WorkshopServices.Add(new WorkshopService { Name = "Taller Pepe" });
                _context.WorkshopServices.Add(new WorkshopService { Name = "Moto Reparo" });
                _context.WorkshopServices.Add(new WorkshopService { Name = "Aceites Moto" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckWorkshopTypesAsync()
        {
            if (!_context.WorkshopTypes.Any())
            {
                _context.WorkshopTypes.Add(new WorkshopType { Name = "Montallatas" });
                _context.WorkshopTypes.Add(new WorkshopType { Name = "Cambio Aceite" });
                _context.WorkshopTypes.Add(new WorkshopType { Name = "Taller" });
                await _context.SaveChangesAsync();
            }
        }


        private async Task CheckConcessionairesAsync()
        {
            if (!_context.Concessionaires.Any())
            {
                _context.Concessionaires.Add(new Concessionaire { Name = "Honda" });
                _context.Concessionaires.Add(new Concessionaire { Name = "Auteco" });
                _context.Concessionaires.Add(new Concessionaire { Name = "AKT" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCraneServicesAsync()
        {
            if (!_context.CraneServices.Any())
            {
                _context.CraneServices.Add(new CraneService { Name = "Grua" });
                _context.CraneServices.Add(new CraneService { Name = "Moto Grua" });
                _context.CraneServices.Add(new CraneService { Name = "Grua Taller" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckPlaceSellingsAsync()
        {
            if (!_context.PlaceSellings.Any())
            {
                _context.PlaceSellings.Add(new PlaceSelling { Name = "Cascos Lucho" });
                _context.PlaceSellings.Add(new PlaceSelling { Name = "Bayadera Shop" });
                _context.PlaceSellings.Add(new PlaceSelling { Name = "Moto Llantas" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckMotorcycleTypesAsync()
        {
            if (!_context.MotorcycleTypes.Any())
            {
                _context.MotorcycleTypes.Add(new MotorcycleType { Name = "Sport" });
                _context.MotorcycleTypes.Add(new MotorcycleType { Name = "Scooter" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckOwnersAsync()
        {
            if (!_context.Owners.Any())
            {
                AddOwner("8989898", "Hernan", "Cordoba", "234 3232", "310 322 3121", "Calle Luna Calle Sol");
                AddOwner("7655544", "Jose", "Cardona", "343 3226", "300 322 3421", "Calle 77 #22 21");
                AddOwner("6565555", "Maria", "López", "450 4332", "350 322 3521", "Carrera 56 #22 21");
                await _context.SaveChangesAsync();
            }
        }

        private void AddOwner(string document, string firstName, string lastName, string fixedPhone, string cellPhone, string address)
        {
            _context.Owners.Add(new Owner
            {
                Address = address,
                CellPhone = cellPhone,
                Document = document,
                FirstName = firstName,
                FixedPhone = fixedPhone,
                LastName = lastName
            });
        }

        private void AddMotorcycle(string name, Owner owner, MotorcycleType motorcycleType, string brand)
        {
            _context.Motorcycles.Add(new Motorcycle
            {
                Shop = DateTime.Now.AddYears(-2),
                Name = name,
                Owner = owner,
                MotorcycleType = motorcycleType,
                Brand = brand
            });
        }

        
        }
    }


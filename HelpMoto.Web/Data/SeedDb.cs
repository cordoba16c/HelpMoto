using HelpMoto.Web.Data.Entities;
using HelpMoto.Web.Herlpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HelpMoto.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context,
             IUserHelper userHelper)
        {
            _dataContext = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("1010", "Hernan", "Cordoba", "cordoba16c@gmail.com", "350 634 2747", "Calle Luna Calle Sol", "Admin");
            var customer = await CheckUserAsync("2020", "Johana", "Gonzalez", "johanacardonag@gmail.com", "350 634 2747", "Calle Luna Calle Sol", "Customer");
            await CheckMotorcycleTypesAsync();
            await CheckPlaceSellingTypesAsync();
            await CheckMotorcyclesAsync();
            await CheckWorkshopServicesAsync();
            await CheckConcessionairesAsync();
            await CheckCraneServicesAsync();
            await CheckOwnerAsync(customer);
            await CheckManagerAsync(manager);
            await CheckPlaceSellingsAsync();
            await CheckWorkshopTypesAsync();
            
        }

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
        }

        private async Task<User> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);

            }

            return user;
        }
        private async Task CheckMotorcyclesAsync()
        {
            if (!_dataContext.Motorcycles.Any())
            { 
            var owner = _dataContext.Owners.FirstOrDefault();
            var motorcycleType = _dataContext.MotorcycleTypes.FirstOrDefault();
                AddMotorcycle("CB1", owner, motorcycleType, "Honda");
                AddMotorcycle("BWIS", owner, motorcycleType, "Yamaha");
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckPlaceSellingTypesAsync()
        {
            if (!_dataContext.PlaceSellingTypes.Any())
            {
                _dataContext.PlaceSellingTypes.Add(new PlaceSellingType { Name = "Cascos" });
                _dataContext.PlaceSellingTypes.Add(new PlaceSellingType { Name = "Repuestos" });
                _dataContext.PlaceSellingTypes.Add(new PlaceSellingType { Name = "Llantas" });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckWorkshopServicesAsync()
        {
            if (!_dataContext.WorkshopServices.Any())
            {
                _dataContext.WorkshopServices.Add(new WorkshopService { Name = "Taller Pepe" });
                _dataContext.WorkshopServices.Add(new WorkshopService { Name = "Moto Reparo" });
                _dataContext.WorkshopServices.Add(new WorkshopService { Name = "Aceites Moto" });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckWorkshopTypesAsync()
        {
            if (!_dataContext.WorkshopTypes.Any())
            {
                _dataContext.WorkshopTypes.Add(new WorkshopType { Name = "Montallatas" });
                _dataContext.WorkshopTypes.Add(new WorkshopType { Name = "Cambio Aceite" });
                _dataContext.WorkshopTypes.Add(new WorkshopType { Name = "Taller" });
                await _dataContext.SaveChangesAsync();
            }
        }


        private async Task CheckConcessionairesAsync()
        {
            if (!_dataContext.Concessionaires.Any())
            {
                _dataContext.Concessionaires.Add(new Concessionaire { Name = "Honda" });
                _dataContext.Concessionaires.Add(new Concessionaire { Name = "Auteco" });
                _dataContext.Concessionaires.Add(new Concessionaire { Name = "AKT" });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckCraneServicesAsync()
        {
            if (!_dataContext.CraneServices.Any())
            {
                _dataContext.CraneServices.Add(new CraneService { Name = "Grua" });
                _dataContext.CraneServices.Add(new CraneService { Name = "Moto Grua" });
                _dataContext.CraneServices.Add(new CraneService { Name = "Grua Taller" });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckPlaceSellingsAsync()
        {
            if (!_dataContext.PlaceSellings.Any())
            {
                _dataContext.PlaceSellings.Add(new PlaceSelling { Name = "Cascos Lucho" });
                _dataContext.PlaceSellings.Add(new PlaceSelling { Name = "Bayadera Shop" });
                _dataContext.PlaceSellings.Add(new PlaceSelling { Name = "Moto Llantas" });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckMotorcycleTypesAsync()
        {
            if (!_dataContext.MotorcycleTypes.Any())
            {
                _dataContext.MotorcycleTypes.Add(new MotorcycleType { Name = "Sport" });
                _dataContext.MotorcycleTypes.Add(new MotorcycleType { Name = "Scooter" });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckOwnerAsync(User user)
        {
            if (!_dataContext.Owners.Any())
            {
                _dataContext.Owners.Add(new Owner { User = user });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckManagerAsync(User user)
        {
            if (!_dataContext.Managers.Any())
            {
                _dataContext.Managers.Add(new Manager { User = user });
                await _dataContext.SaveChangesAsync();
            }
        }


        private void AddMotorcycle(string name, Owner owner, MotorcycleType motorcycleType, string brand)
        {
            _dataContext.Motorcycles.Add(new Motorcycle
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


using HelpMoto.Web.Data.Entities;
using HelpMoto.Web.Helpers;
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
            await CheckMotorcyclesAsync();
            await CheckOwnerAsync(customer);
            await CheckManagerAsync(manager);
            await CheckWorkshopTypesAsync();
           // await CheckWorkshopAsync();

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

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);


            }

            return user;
        }
        private async Task CheckMotorcyclesAsync()
        {
            if (!_dataContext.Motorcycles.Any())
            { 
            var owner = _dataContext.Owners.FirstOrDefault();
            var motorcycleType = _dataContext.MotorcycleTypes.FirstOrDefault();
                AddMotorcycle("Pirula", owner, motorcycleType, "SUZUKI");
                await _dataContext.SaveChangesAsync();
            }
        }

        /*private async Task CheckWorkshopAsync()
        {
            if (!_dataContext.Workshops.Any())
            {
                var owner = _dataContext.Workshops.FirstOrDefault();
                var workshopType = _dataContext.WorkshopTypes.FirstOrDefault();
                AddWorkshop("Centro servicios YAMAHA", "CL 37 #45-15", "Fabian Rios","302 378 56 45", workshopType, "Horario atención lunes a sabado 7:00 am a 5:00 pm");
                await _dataContext.SaveChangesAsync();
            }
        }*/
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

        private async Task CheckMotorcycleTypesAsync()
        {
            if (!_dataContext.MotorcycleTypes.Any())
            {
                _dataContext.MotorcycleTypes.Add(new MotorcycleType { Name = "SPORT" });
                _dataContext.MotorcycleTypes.Add(new MotorcycleType { Name = "SCOOTER" });
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
        private void AddWorkshop(string Name, string Address, string ContactName, string PhoneName, WorkshopType workshopTypes, string Remarks)
        {
            _dataContext.Workshops.Add(new Workshop
            {
                Name = Name,
                Address = Address,
                ContactName = ContactName,
                PhoneName = PhoneName,
                WorkshopType = workshopTypes,
                Remarks = Remarks
            });
        }
    }
    }


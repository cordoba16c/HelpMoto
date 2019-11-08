using HelpMoto.Web.Data;
using HelpMoto.Web.Data.Entities;
using HelpMoto.Web.Helpers;
using HelpMoto.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HelpMoto.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OwnersController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IImageHelper _imageHelper;
       
        public OwnersController(
            DataContext context,
            IUserHelper userHelper,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper,
            IImageHelper imageHelper)
        {
            _dataContext = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
            _imageHelper = imageHelper;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Owners
                .Include(o => o.User)
                .Include(o => o.Motorcycles));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _dataContext.Owners
                .Include(o => o.User)
                .Include(o => o.Motorcycles)
                .ThenInclude(m => m.MotorcycleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Address = model.Address,
                    Document = model.Document,
                    Email = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Username
                };
                var response = await _userHelper.AddUserAsync(user, model.Password);
                if (response.Succeeded)
                {
                    var userInDB = await _userHelper.GetUserByEmailAsync(model.Username);
                    await _userHelper.AddUserToRoleAsync(userInDB, "Customer");

                    var owner = new Owner
                    {
                        Motorcycles = new List<Motorcycle>(),
                        User = userInDB
                    };

                    _dataContext.Owners.Add(owner);

                    try
                    {
                        await _dataContext.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.ToString());
                        return View(model);
                    }

                }
                ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
            }

            return View(model);
        }
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _dataContext.Owners
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Address = owner.User.Address,
                Document = owner.User.Document,
                FirstName = owner.User.FirstName,
                Id = owner.Id,
                LastName = owner.User.LastName,
                PhoneNumber = owner.User.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var owner = await _dataContext.Owners
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                owner.User.Document = model.Document;
                owner.User.FirstName = model.FirstName;
                owner.User.LastName = model.LastName;
                owner.User.Address = model.Address;
                owner.User.PhoneNumber = model.PhoneNumber;

                await _userHelper.UpdateUserAsync(owner.User);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _dataContext.Owners
                .Include(o => o.User)
                .Include(o => o.Motorcycles)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (owner == null)
            {
                return NotFound();
            }
            if (owner.Motorcycles.Count > 0)
            {

                
                return RedirectToAction(nameof(Index));
            }
            await _userHelper.DeleteUserAsync(owner.User.Email);
            _dataContext.Owners.Remove(owner);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddMotorcycle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _dataContext.Owners.FindAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            var model = new MotorcycleViewModel
            {
                Shop = DateTime.Today,
                OwnerId = owner.Id,
                MotorcycleTypes = _combosHelper.GetComboMotorcycleTypes()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMotorcycle(MotorcycleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);
                }

                var motorcycle = await _converterHelper.ToMotorcycleAsync(model, path, true);
                _dataContext.Motorcycles.Add(motorcycle);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"Details/{model.OwnerId}");
            }

            model.MotorcycleTypes = _combosHelper.GetComboMotorcycleTypes();
            return View(model);
        }
        public async Task<IActionResult> EditMotorcycle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await _dataContext.Motorcycles
                .Include(m => m.Owner)
                .Include(m => m.MotorcycleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycle == null)
            {
                return NotFound();
            }

            return View(_converterHelper.ToMotorcycleViewModel(motorcycle));
        }

        [HttpPost]
        public async Task<IActionResult> EditMotorcycle(MotorcycleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = model.ImageUrl;

                if (model.ImageFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile);
                }

                var motorcycle = await _converterHelper.ToMotorcycleAsync(model, path, false);
                _dataContext.Motorcycles.Update(motorcycle);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"Details/{model.OwnerId}");
            }
            model.MotorcycleTypes = _combosHelper.GetComboMotorcycleTypes();
            return View(model);
        }

        public async Task<IActionResult> DetailsMotorcycle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await _dataContext.Motorcycles
                .Include(p => p.Owner)
                .ThenInclude(o => o.User)
                .Include(p => p.Histories)
                .ThenInclude(h => h.WorkshopType)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (motorcycle == null)
            {
                return NotFound();
            }

            return View(motorcycle);
        }
        public async Task<IActionResult> AddHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await _dataContext.Motorcycles.FindAsync(id.Value);
            if (motorcycle == null)
            {
                return NotFound();
            }

            var model = new HistoryViewModel
            {
                InicialDate = DateTime.Now,
                FinalDate = DateTime.Now,
                MotorcycleId = motorcycle.Id,
                WorkshopTypes = _combosHelper.GetComboWorkshopTypes(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddHistory(HistoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var history = await _converterHelper.ToHistoryAsync(model, true);
                _dataContext.Histories.Add(history);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"{nameof(DetailsMotorcycle)}/{model.MotorcycleId}");
            }

            model.WorkshopTypes = _combosHelper.GetComboWorkshopTypes();
            return View(model);
        }

        public async Task<IActionResult> EditHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var history = await _dataContext.Histories
                .Include(h => h.Motorcycle)
                .Include(h => h.WorkshopType)
                .FirstOrDefaultAsync(p => p.Id == id.Value);
            if (history == null)
            {
                return NotFound();
            }

            return View(_converterHelper.ToHistoryViewModel(history));
        }
        [HttpPost]
        public async Task<IActionResult> EditHistory(HistoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var history = await _converterHelper.ToHistoryAsync(model, false);
                _dataContext.Histories.Update(history);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"{nameof(DetailsMotorcycle)}/{model.MotorcycleId}");
            }

            model.WorkshopTypes = _combosHelper.GetComboWorkshopTypes();
            return View(model);
        }
        public async Task<IActionResult> DeleteHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var history = await _dataContext.Histories
                .Include(h => h.Motorcycle)
                .FirstOrDefaultAsync(h => h.Id == id.Value);
            if (history == null)
            {
                return NotFound();
            }

            _dataContext.Histories.Remove(history);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction($"{nameof(DetailsMotorcycle)}/{history.Motorcycle.Id}");
        }

        public async Task<IActionResult> DeleteMotorcycle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await _dataContext.Motorcycles
                .Include(p => p.Owner)
                .Include(p => p.Histories)
                .FirstOrDefaultAsync(p => p.Id == id.Value);
            if (motorcycle == null)
            {
                return NotFound();
            }

            if (motorcycle.Histories.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "The Motorcycle can't be deleted because it has related records.");
                return RedirectToAction($"{nameof(Details)}/{motorcycle.Owner.Id}");
            }

            _dataContext.Motorcycles.Remove(motorcycle);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{motorcycle.Owner.Id}");
        }
    }
}





using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpMoto.Web.Data;
using HelpMoto.Web.Data.Entities;
using HelpMoto.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using HelpMoto.Web.Models;
using System.IO;

namespace HelpMoto.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MotorcyclesController : Controller
    {
        private readonly ICombosHelper _combosHelper;
        private readonly DataContext _dataContext;

        public MotorcyclesController(
            ICombosHelper combosHelper,
            DataContext dataContext)
        {
            _combosHelper = combosHelper;
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View(_dataContext.Motorcycles
                .Include(p => p.Owner)
                .ThenInclude(o => o.User)
                .Include(p => p.MotorcycleType)
                .Include(p => p.Histories));
        }

        public async Task<IActionResult> Details(int? id)
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await _dataContext.Motorcycles
                .Include(p => p.Owner)
                .Include(p => p.MotorcycleType)
                .FirstOrDefaultAsync(p => p.Id == id.Value);
            if (motorcycle == null)
            {
                return NotFound();
            }

            var view = new MotorcycleViewModel
            {
                Shop = motorcycle.Shop,
                Id = motorcycle.Id,
                ImageUrl = motorcycle.ImageUrl,
                Name = motorcycle.Name,
                OwnerId = motorcycle.Owner.Id,
                MotorcycleTypeId = motorcycle.MotorcycleType.Id,
                MotorcycleTypes = _combosHelper.GetComboMotorcycleTypes(),
                Brand = motorcycle.Brand,
                Remarks = motorcycle.Remarks
            };

            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MotorcycleViewModel view)
        {
            if (ModelState.IsValid)
            {
                var path = view.ImageUrl;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Motorcycles",
                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Motorcycles/{file}";
                }

                var motorcycle = new Motorcycle
                {
                    Shop = view.Shop,
                    Id = view.Id,
                    ImageUrl = path,
                    Name = view.Name,
                    Owner = await _dataContext.Owners.FindAsync(view.OwnerId),
                    MotorcycleType = await _dataContext.MotorcycleTypes.FindAsync(view.MotorcycleTypeId),
                    Brand = view.Brand,
                    Remarks = view.Remarks
                };

                _dataContext.Motorcycles.Update(motorcycle);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycle = await _dataContext.Motorcycles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycle == null)
            {
                return NotFound();
            }

            _dataContext.Motorcycles.Remove(motorcycle);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
            return RedirectToAction($"{nameof(Details)}/{history.Motorcycle.Id}");
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

            var view = new HistoryViewModel
            {
                InicialDate = history.InicialDate,
                FinalDate = history.FinalDate,
                Description = history.Description,
                Id = history.Id,
                MotorcycleId = history.Motorcycle.Id,
                Remarks = history.Remarks,
                WorkshopTypeId = history.WorkshopType.Id,
                WorkshopTypes = _combosHelper.GetComboWorkshopTypes()
            };

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> EditHistory(HistoryViewModel view)
        {
            if (ModelState.IsValid)
            {
                var history = new History
                {
                    InicialDate = view.InicialDate,
                    FinalDate = view.FinalDate,
                    Description = view.Description,
                    Id = view.Id,
                    Motorcycle = await _dataContext.Motorcycles.FindAsync(view.MotorcycleId),
                    Remarks = view.Remarks,
                    WorkshopType = await _dataContext.WorkshopTypes.FindAsync(view.WorkshopTypeId)
                };

                _dataContext.Histories.Update(history);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{view.MotorcycleId}");
            }

            return View(view);
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

            var view = new HistoryViewModel
            {
                InicialDate = DateTime.Now,
                FinalDate = DateTime.Now,
                MotorcycleId = motorcycle.Id,
                WorkshopTypes = _combosHelper.GetComboWorkshopTypes(),
            };

            return View(view);
        }

        [HttpPost]
        public async Task<IActionResult> AddHistory(HistoryViewModel view)
        {
            if (ModelState.IsValid)
            {
                var history = new History
                {
                    InicialDate = view.InicialDate,
                    FinalDate = view.FinalDate,
                    Description = view.Description,
                    Motorcycle = await _dataContext.Motorcycles.FindAsync(view.MotorcycleId),
                    Remarks = view.Remarks,
                    WorkshopType = await _dataContext.WorkshopTypes.FindAsync(view.WorkshopTypeId)
                };

                _dataContext.Histories.Add(history);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{view.MotorcycleId}");
            }

            return View(view);
        }
    }
}
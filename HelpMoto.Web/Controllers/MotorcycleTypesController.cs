using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpMoto.Web.Data;
using HelpMoto.Web.Data.Entities;

namespace HelpMoto.Web.Controllers
{
    public class MotorcycleTypesController : Controller
    {
        private readonly DataContext _context;

        public MotorcycleTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: MotorcycleTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MotorcycleTypes.ToListAsync());
        }

        // GET: MotorcycleTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycleType = await _context.MotorcycleTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycleType == null)
            {
                return NotFound();
            }

            return View(motorcycleType);
        }

        // GET: MotorcycleTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MotorcycleTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] MotorcycleType motorcycleType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motorcycleType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(motorcycleType);
        }

        // GET: MotorcycleTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycleType = await _context.MotorcycleTypes.FindAsync(id);
            if (motorcycleType == null)
            {
                return NotFound();
            }
            return View(motorcycleType);
        }

        // POST: MotorcycleTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MotorcycleType motorcycleType)
        {
            if (id != motorcycleType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motorcycleType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotorcycleTypeExists(motorcycleType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(motorcycleType);
        }

        // GET: MotorcycleTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var motorcycleType = await _context.MotorcycleTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (motorcycleType == null)
            {
                return NotFound();
            }

            return View(motorcycleType);
        }

        // POST: MotorcycleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var motorcycleType = await _context.MotorcycleTypes.FindAsync(id);
            _context.MotorcycleTypes.Remove(motorcycleType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotorcycleTypeExists(int id)
        {
            return _context.MotorcycleTypes.Any(e => e.Id == id);
        }
    }
}

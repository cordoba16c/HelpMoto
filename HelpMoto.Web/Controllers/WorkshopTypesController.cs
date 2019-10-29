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
    public class WorkshopTypesController : Controller
    {
        private readonly DataContext _context;

        public WorkshopTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: WorkshopTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkshopTypes.ToListAsync());
        }

        // GET: WorkshopTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshopType = await _context.WorkshopTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workshopType == null)
            {
                return NotFound();
            }

            return View(workshopType);
        }

        // GET: WorkshopTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkshopTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] WorkshopType workshopType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workshopType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workshopType);
        }

        // GET: WorkshopTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshopType = await _context.WorkshopTypes.FindAsync(id);
            if (workshopType == null)
            {
                return NotFound();
            }
            return View(workshopType);
        }

        // POST: WorkshopTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] WorkshopType workshopType)
        {
            if (id != workshopType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workshopType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkshopTypeExists(workshopType.Id))
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
            return View(workshopType);
        }

        // GET: WorkshopTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshopType = await _context.WorkshopTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workshopType == null)
            {
                return NotFound();
            }
            _context.WorkshopTypes.Remove(workshopType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkshopTypeExists(int id)
        {
            return _context.WorkshopTypes.Any(e => e.Id == id);
        }
    }
}

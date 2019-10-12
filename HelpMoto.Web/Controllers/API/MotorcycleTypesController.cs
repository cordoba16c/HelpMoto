using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpMoto.Web.Data;
using HelpMoto.Web.Data.Entities;

namespace HelpMoto.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorcycleTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public MotorcycleTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/MotorcycleTypes
        [HttpGet]
        public IEnumerable<MotorcycleType> GetMotorcycleTypes()
        {
            return _context.MotorcycleTypes;
        }

        // GET: api/MotorcycleTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMotorcycleType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var motorcycleType = await _context.MotorcycleTypes.FindAsync(id);

            if (motorcycleType == null)
            {
                return NotFound();
            }

            return Ok(motorcycleType);
        }

        // PUT: api/MotorcycleTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotorcycleType([FromRoute] int id, [FromBody] MotorcycleType motorcycleType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != motorcycleType.Id)
            {
                return BadRequest();
            }

            _context.Entry(motorcycleType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MotorcycleTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MotorcycleTypes
        [HttpPost]
        public async Task<IActionResult> PostMotorcycleType([FromBody] MotorcycleType motorcycleType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MotorcycleTypes.Add(motorcycleType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMotorcycleType", new { id = motorcycleType.Id }, motorcycleType);
        }

        // DELETE: api/MotorcycleTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotorcycleType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var motorcycleType = await _context.MotorcycleTypes.FindAsync(id);
            if (motorcycleType == null)
            {
                return NotFound();
            }

            _context.MotorcycleTypes.Remove(motorcycleType);
            await _context.SaveChangesAsync();

            return Ok(motorcycleType);
        }

        private bool MotorcycleTypeExists(int id)
        {
            return _context.MotorcycleTypes.Any(e => e.Id == id);
        }
    }
}
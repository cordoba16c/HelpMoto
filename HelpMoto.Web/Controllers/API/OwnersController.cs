using HelpMoto.Common.Models;
using HelpMoto.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpMoto.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly DataContext _context;

        public OwnersController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("GetOwnerByEmail")]
        public async Task<IActionResult> GetOwner(EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var owner = _context.Owners
                .Include(o => o.User)
                .Include(o => o.Motorcycles)
                .ThenInclude(p => p.MotorcycleType)
                .Include(o => o.Motorcycles)
                /*.ThenInclude(p => p.Histories)
                .ThenInclude(h => h.ServiceType)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower() == emailRequest.Email.ToLower());*/
                .FirstOrDefaultAsync(o => o.User.Email == emailRequest.Email);
            if (owner == null)
            {
                return NotFound();
            }
            return Ok(owner);

        }
    }
}

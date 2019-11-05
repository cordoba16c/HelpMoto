using HelpMoto.Common.Models;
using HelpMoto.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
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
                .ThenInclude(p => p.Histories)
                .ThenInclude(h => h.WorkshopType)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower() == emailRequest.Email.ToLower())
                .FirstOrDefaultAsync(o => o.User.Email == emailRequest.Email);

            
            var response = new OwnerResponse
            {
                Id = owner.Id,  
                FirstName = owner.User.FirstName,
                LastName = owner.User.LastName,
                Address = owner.User.Address,
                Document = owner.User.Document,
                Email = owner.User.Email,
                PhoneNumber = owner.User.PhoneNumber,
                Motorcycles = owner.Motorcycles.Select(p => new MotorcycleResponse
                {
                    
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageFullPath,
                    Brand = p.Brand,
                    Shop = p.Shop,
                    Remarks = p.Remarks,
                    ShopLocal = p.ShopLocal,
                    MotorcycleType = p.MotorcycleType.Name,
                    Histories = p.Histories.Select(h => new HistoryResponse
                    {
                        InicialDate = h.InicialDate,
                        FinalDate = h.FinalDate,
                        Description = h.Description,
                        Id = h.Id,
                        Remarks = h.Remarks,
                        WorkshopType = h.WorkshopType.Name
                    }).ToList()
                }).ToList()
            };

            return Ok(response);
        }
    }

}
 

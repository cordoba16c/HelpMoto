using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpMoto.Common.Models;
using HelpMoto.Web.Data;

namespace HelpMoto.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
    public class OwnersController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public OwnersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetOwnerByEmail")]
        public async Task<IActionResult> GetOwner(EmailRequest emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var owner = await _dataContext.Owners
                .Include(o => o.User)
                .Include(o => o.Motorcycles)
                .ThenInclude(p => p.MotorcycleType)
                .Include(o => o.Motorcycles)
                .ThenInclude(p => p.Histories)
                .ThenInclude(h => h.WorkshopType)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower() == emailRequest.Email.ToLower());

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
                    Shop = p.Shop,
                    Id = p.Id,
                    ImageUrl = p.ImageFullPath,
                    Name = p.Name,
                    Remarks = p.Remarks,
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
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HelpMoto.Common.Helpers;
using HelpMoto.Common.Models;
using HelpMoto.Web.Data;
using HelpMoto.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HelpMoto.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkshopController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public WorkshopController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


      /*  [HttpGet]
        public async Task<IActionResult> GetWorkshop()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var workshop = await _dataContext.Workshops.ToListAsync();

            var response = new WorkshopResponse
            {
                Id = workshop.Id
,                Name = workshop.Name,
                Address = workshop.Address,
                ContactName = workshop.ContactName,
                PhoneName= workshop.PhoneName,
                WorkshopType = h.WorkshopType.Name,
                Remarks = workshop.Remarks
            };
            return Ok(response);

        }*/
        [HttpPost]
        public async Task<IActionResult> PostWorkshop([FromBody] WorkshopRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            var WorkshopType = await _dataContext.WorkshopTypes.FindAsync(request.WorkshopTypeId);
            if (WorkshopType == null)
            {
                return BadRequest("Not valid workshop type.");
            }
                      

            var workshop = new Workshop
            {
                
                Name = request.Name,                
                WorkshopType = WorkshopType,
                Address = request.Address,
                ContactName = request.ContactName,
                PhoneName = request.PhoneName,
                Remarks = request.Remarks
            };

            _dataContext.Workshops.Add(workshop);
            await _dataContext.SaveChangesAsync();
            return Ok(workshop);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkshop([FromRoute] int id, [FromBody] WorkshopRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest();
            }

            var oldWorkshop = await _dataContext.Workshops.FindAsync(request.Id);
            if (oldWorkshop == null)
            {
                return BadRequest("Workshop doesn't exists.");
            }

            var WorkshopType = await _dataContext.WorkshopTypes.FindAsync(request.WorkshopTypeId);
            if (WorkshopType == null)
            {
                return BadRequest("Not valid workshop type.");
            }



            oldWorkshop.Name = request.Name;
            oldWorkshop.WorkshopType = WorkshopType;
            oldWorkshop.Address = request.Address;
            oldWorkshop.ContactName = request.ContactName;
            oldWorkshop.PhoneName = request.PhoneName;
            oldWorkshop.Remarks = request.Remarks;

            _dataContext.Workshops.Update(oldWorkshop);
            await _dataContext.SaveChangesAsync();
            return Ok(oldWorkshop);
        }

    }
}
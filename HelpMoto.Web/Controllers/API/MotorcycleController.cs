﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HelpMoto.Common.Helpers;
using HelpMoto.Common.Models;
using HelpMoto.Web.Data;
using HelpMoto.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelpMoto.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MotorcycleController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public MotorcycleController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        public async Task<IActionResult> PostMotorcycle([FromBody] MotorcycleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var owner = await _dataContext.Owners.FindAsync(request.OwnerId);
            if (owner == null)
            {
                return BadRequest("Not valid owner.");
            }

            var MotorcycleType = await _dataContext.MotorcycleTypes.FindAsync(request.MotorcycleTypeId);
            if (MotorcycleType == null)
            {
                return BadRequest("Not valid motorcycle type.");
            }

            var imageUrl = string.Empty;
            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Motorcycles";
                var fullPath = $"~/images/Motorcycles/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                }
            }

            var motorcycle = new Motorcycle
            {
                
                ImageUrl = imageUrl,
                Name = request.Name,
                Owner = owner,
                MotorcycleType = MotorcycleType,
                Brand = request.Brand,
                Remarks = request.Remarks
            };

            _dataContext.Motorcycles.Add(motorcycle);
            await _dataContext.SaveChangesAsync();
            return Ok(motorcycle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMotorcycle([FromRoute] int id, [FromBody] MotorcycleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.Id)
            {
                return BadRequest();
            }

            var oldMotorcycle = await _dataContext.Motorcycles.FindAsync(request.Id);
            if (oldMotorcycle == null)
            {
                return BadRequest("Motorcycle doesn't exists.");
            }

            var MotorcycleType = await _dataContext.MotorcycleTypes.FindAsync(request.MotorcycleTypeId);
            if (MotorcycleType == null)
            {
                return BadRequest("Not valid motorcycle type.");
            }

            var imageUrl = oldMotorcycle.ImageUrl;
            if (request.ImageArray != null && request.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(request.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "wwwroot\\images\\Motorcycles";
                var fullPath = $"~/images/Motorcycles/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    imageUrl = fullPath;
                }
            }


            oldMotorcycle.ImageUrl = imageUrl;
            oldMotorcycle.Name = request.Name;
            oldMotorcycle.MotorcycleType = MotorcycleType;
            oldMotorcycle.Brand = request.Brand;
            oldMotorcycle.Remarks = request.Remarks;
            oldMotorcycle.Shop = request.Shop;

            _dataContext.Motorcycles.Update(oldMotorcycle);
            await _dataContext.SaveChangesAsync();
            return Ok(oldMotorcycle);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMotorcycle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var motorcycle = await _dataContext.Motorcycles
                .Include(p => p.Histories)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (motorcycle == null)
            {
                return NotFound();
            }

            if (motorcycle.Histories.Count > 0)
            {
                return BadRequest("The Motorcycle can't be deleted because it has history.");
            }

            _dataContext.Motorcycles.Remove(motorcycle);
            await _dataContext.SaveChangesAsync();
            return Ok("Motorcycle deleted");
        }

    }
}
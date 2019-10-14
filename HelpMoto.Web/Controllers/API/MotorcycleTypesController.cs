using HelpMoto.Web.Data;
using HelpMoto.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpGet]
        public IEnumerable<MotorcycleType> GetMotorcycleTypes()
        {
            return _context.MotorcycleTypes;
        }
    }
}
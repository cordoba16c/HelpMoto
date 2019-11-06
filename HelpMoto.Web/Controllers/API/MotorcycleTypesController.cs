using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HelpMoto.Web.Data;
using HelpMoto.Web.Data.Entities;

namespace HelpMoto.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MotorcycleTypesController : Controller
    {
        private readonly DataContext _dataContext;

        public MotorcycleTypesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IEnumerable<MotorcycleType> GetMotorcycleTypes()
        {
            return _dataContext.MotorcycleTypes.OrderBy(pt => pt.Name);
        }
    }
}

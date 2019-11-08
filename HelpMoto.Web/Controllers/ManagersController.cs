using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpMoto.Web.Data;
using HelpMoto.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using HelpMoto.Web.Helpers;

namespace HelpMoto.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagersController : Controller
    {
    }
}

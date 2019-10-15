using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpMoto.Web.Herlpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboMotorcycleTypes();
        IEnumerable<SelectListItem> GetComboWorkshopType();
    }
}
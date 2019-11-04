using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpMoto.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboMotorcycleTypes();
    }
}
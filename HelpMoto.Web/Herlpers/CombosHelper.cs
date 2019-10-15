using HelpMoto.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace HelpMoto.Web.Herlpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;
        public CombosHelper(DataContext context)
        {
            _dataContext = context;
        }

        public IEnumerable<SelectListItem> GetComboMotorcycleTypes()
        {
            var list = _dataContext.MotorcycleTypes.Select(mt => new SelectListItem
            {
                Text = mt.Name,
                Value = $"{mt.Id}"
            })
                .OrderBy(mt => mt.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Motorcycle type...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboWorkshopType()
        { 
        var list = _dataContext.WorkshopTypes.Select(wt => new SelectListItem
        {
            Text = wt.Name,
            Value = $"{wt.Id}"
        })
                .OrderBy(wt => wt.Text)
                .ToList();

        list.Insert(0, new SelectListItem
            {
                Text = "[Select a workshop type...]",
                Value = "0"
            });

            return list;
        }
    }
}

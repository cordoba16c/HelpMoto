using HelpMoto.Web.Data;
using HelpMoto.Web.Data.Entities;
using HelpMoto.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpMoto.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(
            DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }

        public async Task<Motorcycle> ToMotorcycleAsync(MotorcycleViewModel model, string path, bool isNew)
        {
            var  motorcycle = new Motorcycle
            {

                Shop = model.Shop,
                Histories = model.Histories,
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                Name = model.Name,
                Owner = await _dataContext.Owners.FindAsync(model.OwnerId),
                MotorcycleType = await _dataContext.MotorcycleTypes.FindAsync(model.MotorcycleTypeId),
                Brand = model.Brand,
                Remarks = model.Remarks
            };

            return motorcycle;

        }
        public MotorcycleViewModel ToMotorcycleViewModel(Motorcycle motorcycle)
        {
            return new MotorcycleViewModel
            {
                Shop = motorcycle.Shop,
                Histories = motorcycle.Histories,
                ImageUrl = motorcycle.ImageUrl,
                Name = motorcycle.Name,
                Owner = motorcycle.Owner,
                MotorcycleType = motorcycle.MotorcycleType,
                Brand = motorcycle.Brand,
                Remarks = motorcycle.Remarks,
                Id = motorcycle.Id,
                OwnerId = motorcycle.Owner.Id,
                MotorcycleTypeId = motorcycle.MotorcycleType.Id,
                MotorcycleTypes = _combosHelper.GetComboMotorcycleTypes()
            };
        }
        public async Task<History> ToHistoryAsync(HistoryViewModel model, bool isNew)
        {
            return new History
            {
                InicialDate = model.InicialDate.ToUniversalTime(),
                FinalDate = model.FinalDate.ToUniversalTime(),
                Description = model.Description,
                Id = isNew ? 0 : model.Id,
                Motorcycle = await _dataContext.Motorcycles.FindAsync(model.MotorcycleId),
                Remarks = model.Remarks,
                WorkshopType = await _dataContext.WorkshopTypes.FindAsync(model.WorkshopTypeId)
            };
        }

        public HistoryViewModel ToHistoryViewModel(History history)
        {
            return new HistoryViewModel
            {
                InicialDate = history.InicialDate,
                FinalDate = history.FinalDate,
                Description = history.Description,
                Id = history.Id,
                MotorcycleId = history.Motorcycle.Id,
                Remarks = history.Remarks,
                WorkshopTypeId = history.WorkshopType.Id,
                WorkshopTypes = _combosHelper.GetComboWorkshopTypes()
            };
        }

    }
}
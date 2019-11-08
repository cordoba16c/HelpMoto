using HelpMoto.Web.Data.Entities;
using HelpMoto.Web.Models;
using System.Threading.Tasks;

namespace HelpMoto.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<Motorcycle> ToMotorcycleAsync(MotorcycleViewModel model, string path, bool isNew);
        MotorcycleViewModel ToMotorcycleViewModel(Motorcycle motorcycle);
        Task<History> ToHistoryAsync(HistoryViewModel model, bool isNew);

        HistoryViewModel ToHistoryViewModel(History history);

    }
}
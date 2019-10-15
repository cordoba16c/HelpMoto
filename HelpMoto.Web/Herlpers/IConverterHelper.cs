using System.Threading.Tasks;
using HelpMoto.Web.Data.Entities;
using HelpMoto.Web.Models;

namespace HelpMoto.Web.Herlpers
{
    public interface IConverterHelper
    {
        Task<Motorcycle> ToMotorcycleAsync(MotorcycleViewModel model, string path, bool isNew);
        MotorcycleViewModel ToMotorcycleViewModel(Motorcycle motorcycle);

        Task<History> ToHistoryAsync(HistoryViewModel model, bool isNew);

        HistoryViewModel ToHistoryViewModel(History history);
    }
}
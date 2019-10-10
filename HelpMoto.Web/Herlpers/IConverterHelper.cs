using System.Threading.Tasks;
using HelpMoto.Web.Data.Entities;
using HelpMoto.Web.Models;

namespace HelpMoto.Web.Herlpers
{
    public interface IConverterHelper
    {
        Task<Motorcycle> ToMotorcycleAsync(MotorcycleViewModel model, string path, bool isNew);
        MotorcycleViewModel ToMotorcycleViewModel(Motorcycle motorcycle);
    }
}
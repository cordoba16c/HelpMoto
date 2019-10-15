using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HelpMoto.Web.Herlpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile);
    }
}
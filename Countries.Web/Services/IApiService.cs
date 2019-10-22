using Countries.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Web.Services
{
    public class IApiService
    {
        Task<Response> GetListAsync<T>(string urlBase,
            string servicePrefix,
            string controller);
    }
}

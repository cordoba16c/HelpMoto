using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelpMoto.Common.Services
{
    public interface IApiService
    {
        Task<bool> CheckConnection(string url);
    }
}

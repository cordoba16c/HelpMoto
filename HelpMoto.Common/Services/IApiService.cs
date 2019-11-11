using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HelpMoto.Common.Models;

namespace HelpMoto.Common.Services
{
    public interface IApiService
    {
        Task<Response> GetOwnerByEmailAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            string email);

        Task<Response> GetTokenAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            TokenRequest request);

        Task<bool> CheckConnection(string url);

        Task<Response> PutAsync<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model,
            string tokenType,
            string accessToken);

        Task<Response> ChangePasswordAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            ChangePasswordRequest changePasswordRequest,
            string tokenType,
            string accessToken);
    }
}

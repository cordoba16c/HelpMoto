using HelpMoto.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HelpMoto.Web.Herlpers
{
    interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);
    }
}
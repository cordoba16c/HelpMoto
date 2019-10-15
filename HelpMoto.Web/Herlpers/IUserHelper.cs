using HelpMoto.Web.Data.Entities;
using HelpMoto.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HelpMoto.Web.Herlpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);
        Task AddUserToRoleAsync(User user, string roleName);
        Task<bool> IsUserInRoleAsync(User user, string roleName);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<bool> DeleteUserAsync(string email);
        Task<IdentityResult> UpdateUserAsync(User user);
    }

}
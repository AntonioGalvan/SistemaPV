using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SistemaPV.Helpers
{
    using Data.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IUserHelper
    {
        Task<CUser> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(CUser user, string password);

        Task<SignInResult> LoginAsync(string username, string password, bool rememberMe);

        Task LogoutAsync();

        Task<IdentityResult> UpdateUserAsync(CUser user);

        Task<IdentityResult> ChangePasswordAsync(CUser user, string oldPassword, string newPassword);

        Task<SignInResult> ValidatePasswordAsync(CUser user, string password);

        Task CheckRoleAsync(string roleName);

        IEnumerable<SelectListItem> GetComboRoles();

        Task AddUserToRoleAsync(CUser user, string roleName);

        Task<bool> IsUserInRoleAsync(CUser user, string roleName);

        Task<CUser> GetUserByIdAsync(string userId);

        Task<List<CUser>> GetAllUsersAsync();

        Task RemoveUserFromRoleAsync(CUser user, string roleName);

        Task DeleteUserAsync(CUser user);
    }
}

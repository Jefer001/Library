using Library_Loan.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace Library_Loan.Helpers
{
    public interface IUserHelpers
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task AddRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task LogoutAsync();
    }
}

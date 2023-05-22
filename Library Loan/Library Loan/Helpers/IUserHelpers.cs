using Library_Loan.DAL.Entities;
using Library_Loan.Models;
using Microsoft.AspNetCore.Identity;

namespace Library_Loan.Helpers
{
    public interface IUserHelpers
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<User> AddUserAsync(AddUserViewModel addUserViewModel);

        Task AddRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel loginViewModel);

        Task LogoutAsync();
    }
}

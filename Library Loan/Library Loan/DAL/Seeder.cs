using Library_Loan.DAL.Entities;
using Library_Loan.Enum;
using Library_Loan.Helpers;

namespace Library_Loan.DAL
{
    public class Seeder
    {
        #region Constants
        private readonly DataBaseContext _context;
        private readonly IUserHelpers _userHelper;
        #endregion

        #region Builder
        public Seeder(DataBaseContext context, IUserHelpers userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }
        #endregion

        #region Public methods
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await PopulateRolesAsync();
            await PopulateUserAsync("Admin", "Library", "admin@yopmail.com", "3002323232", "Street Apple", "102030", "admin_use.png", UserType.Admin);
            await PopulateUserAsync("User", "Estuduante", "user@yopmail.com", "4005656656", "Street Microsoft", "405060", "user.png", UserType.User);

            await _context.SaveChangesAsync();
        }
        #endregion

        #region Private methods
        private async Task PopulateRolesAsync()
        {
            await _userHelper.AddRoleAsync(UserType.Admin.ToString());
            await _userHelper.AddRoleAsync(UserType.User.ToString());
        }

        private async Task PopulateUserAsync(string firstName, string lastName, string email, string phone, string address, string document, string image, UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);

            if (user == null)
            {
                //Guid imageId = await _azureBlobHelper.UploadAzureBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\users\\{image}", "users");
                user = new User
                {
                    Document = document,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    //ImageId = imageId,
                    UserType = userType
                };
                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }
        }
        #endregion
    }
}

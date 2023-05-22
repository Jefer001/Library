using Library_Loan.DAL;
using Library_Loan.DAL.Entities;
using Library_Loan.Helpers;
using Library_Loan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Loan.Controllers
{
    public class UsersController : Controller
    {
        #region Constants
        private readonly DataBaseContext _context;
        private readonly IUserHelpers _userHelpers;
        private readonly IAzureBlobHelper _azureBlobHelper;
        #endregion

        #region Builder
        public UsersController(DataBaseContext context, IUserHelpers userHelpers, IAzureBlobHelper azureBlobHelper)
        {
            _context = context;
            _userHelpers = userHelpers;
            _azureBlobHelper = azureBlobHelper;
        }
        #endregion

        #region User actions
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return _context.Users != null ?
                       View(await _context.Users.ToListAsync()) :
                       Problem("Entity set 'DataBaseContext.Users'  is null.");
        }

        [HttpGet]
        public async Task<IActionResult> CreateAdmin()
        {
            AddUserViewModel addUserViewModel = new()
            {
                Id = Guid.Empty,
                UserType = Enum.UserType.Admin
            };
            return View(addUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (addUserViewModel.ImageFile != null)
                    imageId = await _azureBlobHelper.UploadAzureBlobAsync(addUserViewModel.ImageFile, "users");

                addUserViewModel.ImageId = imageId;
                //addUserViewModel.CreatedDate = DateTime.Now;

                User user = await _userHelpers.AddUserAsync(addUserViewModel);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Este correo ya está siendo usado.");
                    return View(addUserViewModel);
                }
                return RedirectToAction("Index", "Users");
            }
            return View(addUserViewModel);
        }
        #endregion
    }
}

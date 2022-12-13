using InvoicingWebCore.Interfaces;
using InvoicingWebCore.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using InvoicingWebCore.ViewModel;
using System.Threading.Tasks;

namespace InvoicingWebCore.Services
{
    public class UserService : IUserService
    {
        private readonly IDatabaseService _databaseService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IDatabaseService databaseService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _databaseService = databaseService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public ApplicationUser GetLoggedUser(string userId)
        {
            try
            {
                return _databaseService.GetUserById(userId);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public void AddUserSession(ApplicationUser loggedUser)
        {            
            //HttpContext.SetString("UserSession", JsonConvert.SerializeObject(loggedUser));
        }

        public async Task<bool> RegisterUserAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Login };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                return true;                
            }
            return false;
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);

            return result.Succeeded;
        }

        public async void LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}

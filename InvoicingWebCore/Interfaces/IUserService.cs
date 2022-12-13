using InvoicingWebCore.Models;
using InvoicingWebCore.ViewModel;
using System.Security.Claims;

namespace InvoicingWebCore.Interfaces
{
    public interface IUserService
    {
        ApplicationUser GetLoggedUser(string userId);
        void AddUserSession(ApplicationUser loggedUser);
        Task<bool> RegisterUserAsync(RegisterViewModel model);
        Task<bool> LoginAsync(LoginViewModel model);
        void LogoutAsync();
    }
}

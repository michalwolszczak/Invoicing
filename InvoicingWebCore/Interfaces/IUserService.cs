using InvoicingWebCore.Models;
using System.Security.Claims;

namespace InvoicingWebCore.Interfaces
{
    public interface IUserService
    {
        ApplicationUser GetLoggedUser(string userId);
        void AddUserSession(ApplicationUser loggedUser);
    }
}

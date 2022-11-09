using InvoicingWebCore.Interfaces;
using InvoicingWebCore.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingWebCore.Services
{
    public class UserService : IUserService
    {
        private readonly IDatabaseService _databaseService;
        public UserService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
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
    }
}

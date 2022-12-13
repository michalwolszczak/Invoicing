using InvoicingWebCore.Data;
using InvoicingWebCore.Interfaces;
using InvoicingWebCore.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoicingWebCore.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger _logger;
        public DatabaseService(ApplicationDbContext db/*, ILogger logger*/)
        {
            _db = db;
            //_logger = logger;
        }
        public ApplicationUser GetUserById(string userId)
        {
            try
            {
                return _db.Users.FirstOrDefault(x => x.Id == userId);                
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Cannot get user from database");
                throw;
            }                        
        }
    }
}

using InvoicingWebCore.Models;

namespace InvoicingWebCore.Interfaces
{
    public interface IDatabaseService
    {
        ApplicationUser GetUserById(string userId);
    }
}

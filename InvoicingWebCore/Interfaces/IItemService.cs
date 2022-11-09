using InvoicingWebCore.Models;

namespace InvoicingWebCore.Interfaces
{
    public interface IItemService
    {
        IEnumerable<Product> GetAll(ApplicationUser loggedUser);
    }
}

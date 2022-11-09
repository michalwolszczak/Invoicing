using InvoicingWebCore.Models;
using InvoicingWebCore.ViewModel;

namespace InvoicingWebCore.Interfaces
{
    public interface ISessionService
    {
        bool CreateInvoice(string invoiceModel, ApplicationUser loggedUser);
        Invoice UpdateInvoice(InvoiceModel model);
        ApplicationUser GetLoggedUser(string userId);
        IEnumerable<Invoice> GetAllInvoices(ApplicationUser loggerUser);
        IEnumerable<Product> GetAllProducts(ApplicationUser loggerUser);
        IEnumerable<Contractor> GetAllContractors(ApplicationUser loggerUser);

    }
}

using InvoicingWebCore.Models;
using InvoicingWebCore.ViewModel;

namespace InvoicingWebCore.Interfaces
{
    public interface IInvoiceService
    {
        bool Create(string invoiceModel, ApplicationUser loggedUser);
        void CreateInvoiceProducts(List<InvoiceProduct> invoiceProducts, int invoiceId);
        bool Update(Invoice invoice, ApplicationUser loggedUser);
        bool Delete(int invoiceId);         
        Invoice Get(int invoiceId);
        IEnumerable<Invoice> GetAll(ApplicationUser loggedUser) ;
        string GetInvoiceNumber(ApplicationUser loggedUser);

        IEnumerable<InvoiceType> GetInvoiceTypes();
        IEnumerable<TaxType> GetTaxTypes();

    }
}

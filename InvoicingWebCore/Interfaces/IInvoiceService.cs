using InvoicingWebCore.Models;
using InvoicingWebCore.ViewModel;

namespace InvoicingWebCore.Interfaces
{
    public interface IInvoiceService
    {
        bool Create(string invoiceModel);
        void CreateInvoiceProducts(List<InvoiceProduct> invoiceProducts, int invoiceId);
        bool Update(Invoice invoice);
        bool Delete(int invoiceId);         
        Invoice Get(int invoiceId);
        IEnumerable<Invoice> GetAll() ;
        string GetInvoiceNumber();

        IEnumerable<InvoiceType> GetInvoiceTypes();
        IEnumerable<TaxType> GetTaxTypes();

    }
}

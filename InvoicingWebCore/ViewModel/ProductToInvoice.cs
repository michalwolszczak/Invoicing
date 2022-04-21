using InvoicingWebCore.Models;

namespace InvoicingWebCore.ViewModel
{
    public class ProductToInvoice
    {
        public List<Product> Products { get; set; }
        public int InvoiceId { get; set; }
    }
}

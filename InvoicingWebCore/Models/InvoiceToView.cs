namespace InvoicingWebCore.Models
{
    public class InvoiceToView
    {
        public Invoice Invoice { get; set; }
        public List<Contractor> Contractors { get; set; }
        public List<Product> Products { get; set; }
        public List<InvoiceType> InvoiceTypes { get; set; }

        public InvoiceType InvoiceType { get; set; }
    }
}

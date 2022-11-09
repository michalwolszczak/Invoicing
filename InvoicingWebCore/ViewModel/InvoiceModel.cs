using InvoicingWebCore.Models;

namespace InvoicingWebCore.ViewModel
{
    public class InvoiceModel
    {
        public string Number { get; set; }
        public Company Company { get; set; }
        public int ContractorId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime SaleDate { get; set; }
        public int InvoiceTypeId { get; set; }
        public List<InvoiceProduct>? Products { get; set; } = new List<InvoiceProduct>();
    }
}

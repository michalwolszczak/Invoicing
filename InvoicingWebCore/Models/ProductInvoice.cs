using System.ComponentModel.DataAnnotations;

namespace InvoicingWebCore.Models
{
    public class ProductInvoice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int InvoiceId { get; set; }
        public double Discount { get; set; }
    }
}

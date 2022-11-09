using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InvoicingWebCore.Models
{
    public class InvoiceProduct
    {
        [Key]
        public int Id { get; set; }
        public int? InvoiceId { get; set; }
        [JsonIgnore]
        public Invoice? Invoice { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public decimal TotalGross { get; set; }
        public decimal TotalNet { get; set; }
        public decimal NetPrice { get; set; }
        public int Tax { get; set; }
        public string? QuantityUnit { get; set; }
        public int Quantity { get; set; }
    }
}

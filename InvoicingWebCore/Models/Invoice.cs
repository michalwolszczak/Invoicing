using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InvoicingWebCore.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int InvoiceTypeId { get; set; }
        [JsonIgnore]
        public InvoiceType InvoiceType { get; set; }
        public int? ContractorId { get; set; }
        [JsonIgnore]
        public Contractor? Contractor { get; set; }
        public int CompanyId { get; set; }
        [JsonIgnore]
        public Company Company { get; set; }
        [Required]
        public string Number { get; set; }
        //[Required]
        //public int Tax { get; set; }
        [Required]
        [DisplayName("Issue date")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required]
        [DisplayName("Sale date")]
        public DateTime SaleDate { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}

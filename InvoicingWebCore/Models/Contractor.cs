using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InvoicingWebCore.Models
{
    public class Contractor
    {
        public int Id { get; set; }
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        [JsonIgnore]
        public Company Company { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(10)]
        public string? KRS { get; set; }
        [MaxLength(9)]
        public string? Regon { get; set; }
        [Required(ErrorMessage = "The NIP is required")]
        
        [MaxLength(10)]
        public string? NIP { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }

    }
}

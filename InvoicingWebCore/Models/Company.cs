using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InvoicingWebCore.Models
{
    public class Company
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser? User { get; set; }
        [Required]
        public List<Invoice>? Invoices { get; set; } = new List<Invoice>();
        public List<Product>? Products { get; set; } = new List<Product>();
        public List<Contractor>? Contractor { get; set; } = new List<Contractor>();
        [Required]
        public string Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        [MaxLength(10)]
        public string KRS { get; set; }
        [MaxLength(9)]
        public string Regon { get; set; }
        [Required]
        [MaxLength(10)]
        public string NIP { get; set; }
        [Required]
        public int InvoiceNumberCounter { get; set; } = 0;
        [Required]
        public int MonthMumber { get; set; } = 0;

        //address
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string AddressLine2 { get; set; }

    }
}

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
        public string KRS { get; set; }
        [MaxLength(9)]
        public string Regon { get; set; }
        [Required]
        [MaxLength(10)]
        public string NIP { get; set; }


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

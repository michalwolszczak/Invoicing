using System.ComponentModel.DataAnnotations;

namespace InvoicingWebCore.Models
{
    public class Contractor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AddressId { get; set; }
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
        [Required]
        public string Name { get; set; }
        [MaxLength(10)]
        public string KRS { get; set; }
        [MaxLength(9)]
        public string Regon { get; set; }
        [Required]
        [MaxLength(10)]
        public string NIP { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace InvoicingWebCore.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TypeId { get; set; }
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public int Tax { get; set; }
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now; 
        public DateTime SaleDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace InvoicingWebCore.Models
{
    public class InvoiceType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }        
    }
}

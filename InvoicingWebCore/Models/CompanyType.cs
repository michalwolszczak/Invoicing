using System.ComponentModel.DataAnnotations;

namespace InvoicingWebCore.Models
{
    public class CompanyType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

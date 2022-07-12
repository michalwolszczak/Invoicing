using System.ComponentModel.DataAnnotations;

namespace InvoicingWebCore.Models
{
    public class TaxType
    {
        [Key]
        public int Id { get; set; }
        public int Tax { get; set; }
    }
}

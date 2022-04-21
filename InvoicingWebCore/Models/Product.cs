using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InvoicingWebCore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Nazwa produktu")]
        public string Name { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
        [DisplayName("Cena brutto")]
        public double GrossPrice { get; set; }
        [Required]
        [DisplayName("Cena netto")]
        public double NetPrice { get; set; }
        [Required]
        [DisplayName("VAT")]
        public int Tax { get; set; }
    }
}

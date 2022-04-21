using System.ComponentModel.DataAnnotations;

namespace InvoicingWebCore.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public int HomeNumber { get; set; }
        public int BuildingNumber { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Voivodeship { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}

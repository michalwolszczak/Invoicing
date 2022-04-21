using System.ComponentModel.DataAnnotations;

namespace InvoicingWebCore.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public int AddressId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(10)]
        public string KRS { get; set; }
        [MaxLength(9)]
        public string Regon { get; set; }
        [Required]
        [MaxLength(10)]
        public string NIP { get; set; }

    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InvoicingWebCore.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public List<Company> Companies { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace InvoicingWebCore.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

    }
}

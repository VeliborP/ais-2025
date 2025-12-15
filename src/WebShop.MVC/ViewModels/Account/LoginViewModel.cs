using System.ComponentModel.DataAnnotations;

namespace WebShop.MVC.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Lozinka je obavezna")]
        public string Password { get; set; } = "";
    }
}

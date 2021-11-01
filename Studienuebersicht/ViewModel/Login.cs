using System.ComponentModel.DataAnnotations;

namespace Studienuebersicht.MVC.ViewModel
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

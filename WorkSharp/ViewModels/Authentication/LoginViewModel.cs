using System.ComponentModel.DataAnnotations;

namespace WorkSharp.ViewModels.Authentication
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
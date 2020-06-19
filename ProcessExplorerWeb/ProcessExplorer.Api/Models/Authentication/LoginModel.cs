using System.ComponentModel.DataAnnotations;

namespace ProcessExplorer.Api.Models.Authentication
{
    public class LoginModel
    {
        [Required]
        public string Identifier { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

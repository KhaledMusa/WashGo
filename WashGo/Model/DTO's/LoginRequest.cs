using System.ComponentModel.DataAnnotations;

namespace WashGo.Model
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

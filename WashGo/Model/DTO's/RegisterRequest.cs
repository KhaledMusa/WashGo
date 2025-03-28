using System.ComponentModel.DataAnnotations;

namespace WashGo.Model
{
    public class RegisterRequest
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
    }
}

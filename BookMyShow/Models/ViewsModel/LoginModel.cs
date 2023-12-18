using System.ComponentModel.DataAnnotations;

namespace BookMyShow.Models.ViewsModel
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}

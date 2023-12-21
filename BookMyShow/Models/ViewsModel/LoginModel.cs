using System.ComponentModel.DataAnnotations;

namespace BookMyShow.Models.ViewsModel
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

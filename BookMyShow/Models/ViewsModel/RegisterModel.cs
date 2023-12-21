using System.ComponentModel.DataAnnotations;

namespace BookMyShow.Models.ViewsModel
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set;}

        [Required]
        //[RegularExpression(@"1-2")]
        public string RoleId { get; set; }

    }
}

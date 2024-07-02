using System.ComponentModel.DataAnnotations;
using WebLNCTAssistant.Areas.Users.Models;
using WebLNCTAssistant.Models;

namespace WebLNCTAssistant.Areas.AdminUsers.Models
{
    public class AdminUsersModel : BaseModel
    {
        public long? AdminUserID { get; set; }
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please enter email.", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Please enter valid email.")]
        [MaxLength(255, ErrorMessage = "Email address should not be more than 255 charecters.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please enter password.")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Please enter captcha.")]
        public string? Captcha { get; set; }
        public int? Status { get; set; }

    }
}

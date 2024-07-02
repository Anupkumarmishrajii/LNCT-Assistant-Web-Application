using System.ComponentModel.DataAnnotations;
using WebLNCTAssistant.Models;

namespace WebLNCTAssistant.Areas.Users.Models
{
    public class UsersModel : BaseModel
    {
        public long? UsersID { get; set; }

        [Required(ErrorMessage = "Please enter name.", AllowEmptyStrings = false)]
        [MaxLength(400, ErrorMessage = "Name should not be more than 400 charecters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please enter email.", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Please enter valid email.")]
        [MaxLength(255, ErrorMessage = "Email address should not be more than 255 charecters.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please choose Yes or No.")]
        public bool IsLNCTIan { get; set; }
        [Required(ErrorMessage = "Please choose one of thses.")]
        public string? As_a { get; set; }
        [Required(ErrorMessage = "Please choose one of thses.")]
        public string? HigherEaducation { get; set; }
        [Required(ErrorMessage = "Please enter mobile number such that out team will reach to you soon.", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "mobile number shold be only in numeric values.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Mobile number should be between 10 and 15 digits.")] 
        public string? Mobile { get; set; }
        public string? Status { get; set; }

    }
}

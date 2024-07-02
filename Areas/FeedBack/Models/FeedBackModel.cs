using System.ComponentModel.DataAnnotations;
using WebLNCTAssistant.Models;

namespace WebLNCTAssistant.Areas.FeedBack.Models
{
    public class FeedBackModel :BaseModel
    {
        public long? FeedBackID { get; set; }
        [Required(ErrorMessage = "Please enter name.", AllowEmptyStrings = false)]
        [MaxLength(4000, ErrorMessage = "Name should not be more than 4000 charecters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please enter email.", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Please enter valid email.")]
        [MaxLength(255, ErrorMessage = "Email address should not be more than 255 charecters.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please enter mobile number.", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "mobile number shold be only in numeric values.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Mobile number should be between 10 and 15 digits.")]
        public string? Mobile { get; set; }
        [Required(ErrorMessage = "Please choose feedback type", AllowEmptyStrings = false)]
        public string? FeedBackType { get; set; }
        [Required(ErrorMessage = "Please subject name.", AllowEmptyStrings = false)]
        [MaxLength(400, ErrorMessage = "Subject name should not be more than 400 charecters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid subject name")]
        public string? Subject { get; set; }
        [Required(ErrorMessage = "Please enter feedBack message.", AllowEmptyStrings = false)]
        [MaxLength(4000, ErrorMessage = "FeedBack message name should not be more than 4000 charecters.")]
        public string? FeedBackMessage { get; set; }
        [Required(ErrorMessage = "Please choose priority level.", AllowEmptyStrings = false)]
        public string? Priority_Level { get; set; }
        public string? Status { get; set; }
    }
}

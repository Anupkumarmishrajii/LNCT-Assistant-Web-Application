using System.ComponentModel.DataAnnotations;
using WebLNCTAssistant.Models;

namespace WebLNCTAssistant.Areas.Students.Models
{
    public class StudentsModel : BaseModel
    {
        public long? StudentID { get; set; }
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please feel Email.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please feel Password.")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Please enter captcha.")]
        public string? Captcha { get; set; }
        public int? Status { get; set; }
    }
}

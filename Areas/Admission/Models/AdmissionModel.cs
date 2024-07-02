using System.ComponentModel.DataAnnotations;
using WebLNCTAssistant.Models;

namespace WebLNCTAssistant.Areas.Admission.Models
{
    public class AdmissionModel : BaseModel, IValidatableObject
    {
        public long? AdmissionID { get; set; }
        [Required(ErrorMessage = "Please enter name.", AllowEmptyStrings = false)]
        [MaxLength(4000, ErrorMessage = "Name should not be more than 4000 charecters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please enter email.", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Please enter valid email.")]
        [MaxLength(255, ErrorMessage ="Email address should not be more than 255 charecters.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please enter date of birth.", AllowEmptyStrings = false)]
        [DataType(DataType.DateTime)]
        public DateTime? DOB { get; set; }
        [Required(ErrorMessage = "Please mention gender.", AllowEmptyStrings = false)]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Please mention disability.", AllowEmptyStrings = false)]
        public bool Disability { get; set; }
        [Required(ErrorMessage = "Please enter address.", AllowEmptyStrings = false)]
        [MaxLength(4000,ErrorMessage = "Address should not be more than 4000 charecters.")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Please enter mobile number.", AllowEmptyStrings = false)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "mobile number shold be only in numeric values.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Mobile number should be between 10 and 15 digits.")]
        public string? Mobile { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile number shold be only in numeric values.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Mobile number should be between 10 and 15 digits.")]
        public string? AlternateMobile { get; set; }
        [Required(ErrorMessage = "Please enter previous institute name.", AllowEmptyStrings = false)]
        [MaxLength(400, ErrorMessage = "Previous institute should not be more than 4000 charecters.")]
        public string? Previous_Institute { get; set; }
        [Required(ErrorMessage = "Please enter passed out year.", AllowEmptyStrings = false)]
        [Range(1000,9999, ErrorMessage ="Pass out year should be of 4 digited value.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Pass out year shold be only in numeric values.")]
        public int? Year_PassedOut { get; set; }
        [Required(ErrorMessage = "Please enter gained score.", AllowEmptyStrings = false)]
        [MaxLength(50, ErrorMessage = "Score should not be more than 50 charecters.")]
        public string? Score { get; set; }
        [Required(ErrorMessage = "Please enter desired program.", AllowEmptyStrings = false)]
        [MaxLength(400, ErrorMessage = "Desired programshould not be more than 4000 charecters.")]
        public string? Desired_Program { get; set; }
        [Required(ErrorMessage = "Please choose any language.", AllowEmptyStrings = false)]
        public string? Language { get; set; }
        public string? Reference { get; set; }
        public string? Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DOB != null && DOB >= DateTime.Now.AddYears(-10))
            {
                yield return new ValidationResult("Please enter your correct date of birth.", new[] { nameof(DOB) });
            }

            if (Year_PassedOut != null && Year_PassedOut <= 2020 && Year_PassedOut > 2024)
            {
                yield return new ValidationResult("Pass out year shold be not less than 2020 as well as not more than 2024.", new[] {nameof(Year_PassedOut)});
            }
        }
    }
}

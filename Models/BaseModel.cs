namespace WebLNCTAssistant.Models
{
    public class BaseModel
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public bool IsShowValidation { get; set; }
        public bool Isshowsuccess { get; set; }
        public bool Done { get; set; } = false;
    }
}

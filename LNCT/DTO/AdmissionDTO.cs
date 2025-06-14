using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DTO
{
    public class AdmissionDTO : BaseDTO
    {
        public long? AdmissionID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public int? Disability { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public string? AlternateMobile { get; set; }
        public string? Previous_Institute { get; set; }
        public int? Year_PassedOut { get; set; }
        public string? Score { get; set; }
        public string? Desired_Program { get; set; }
        public string? Language { get; set; }
        public string? Reference { get; set; }


        public string? Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BaseDTO
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public bool IsShowValidation { get; set; }
        public bool Isshowsuccess { get; set; }
        public bool Done { get; set; } = false;
    }
}

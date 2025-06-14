using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UsersDTO : BaseDTO
    {
        public long? UsersID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? IsLNCTIan { get; set; }

        public string? As_a { get; set;}
        public string? HigherEaducation { get; set;}
        public string? Mobile {  get; set; }
        public string? Status { get; set; }
    }
}

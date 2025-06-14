using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FeedBackDTO : BaseDTO
    {
        public long? FeedBackID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? FeedBackType { get; set; }
        public string? Subject { get; set; }
        public string? FeedBackMessage { get; set; }
        public string? Priority_Level { get; set; }
        public string? Status { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparly.Data.Dtos
{
    public class RapidApiResponseDto
    {
        public double similarity { get; set; }
        public double value { get; set; }
        public string version { get; set; }
        public string author { get; set; }
        public string email { get; set; }
        public string result_code { get; set; }
        public string result_msg { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparly.Data.Dtos
{
    public class ComparisonHistoryDto
    {
        public string FirstStudentsName { get; set; }
        public string SecondStudentsName { get; set; }
        public string DocumentAUrl { get; set; }
        public string DocumentBUrl { get; set; }
    }
}

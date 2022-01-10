using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparly.Data.Models
{
    public class Submission
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstStudentsName { get; set; }
        public string SecondStudentsName { get; set; }
        public IFormFile DocumentAUrl { get; set; }
        public IFormFile DocumentBUrl { get; set; }
        public decimal PercentageSimilarity { get; set; }
        public string TextExplanation { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string SubmittedBy { get; set; }
    }
}

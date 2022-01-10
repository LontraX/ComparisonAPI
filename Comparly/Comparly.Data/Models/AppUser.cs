using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparly.Data.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            submissions = new List<Submission>();
        }
        public string FirstName  { get; set; }
        public string LastName  { get; set; }

        List<Submission> submissions { get; set; } 
    }
}

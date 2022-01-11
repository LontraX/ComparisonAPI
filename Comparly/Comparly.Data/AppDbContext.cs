using Comparly.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparly.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        private readonly DbContextOptions<AppDbContext> _options;
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            _options = options;
        }

        public DbSet<Submission> submissions { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server = .; Database = CompareAPI; Integrated Security = True");
        //    base.OnConfiguring(optionsBuilder);
        //}

    }
}

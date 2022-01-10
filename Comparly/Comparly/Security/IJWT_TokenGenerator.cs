using Comparly.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comparly.Core.Security
{
    public interface IJWT_TokenGenerator
    {
        public Task<string> GenerateToken(AppUser user);
    }
}

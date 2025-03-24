using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _4ThWallCafe.Data
{
    public class IdentitySetupDBContext : IdentityDbContext
    {
        public IdentitySetupDBContext(DbContextOptions<IdentitySetupDBContext> options) : base(options) 
        { 
        }
    }
}

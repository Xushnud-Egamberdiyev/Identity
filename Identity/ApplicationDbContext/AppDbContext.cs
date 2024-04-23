using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.ApplicationDbContext
{
    public class AppDbContext : IdentityDbContext<ApUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) 
        {
            
        }
    }
}

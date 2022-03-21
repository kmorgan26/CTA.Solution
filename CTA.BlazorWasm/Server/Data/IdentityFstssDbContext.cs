using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CTA.BlazorWasm.Server.Data
{
    public class IdentityFstssDbContext : IdentityDbContext
    {
        public IdentityFstssDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}

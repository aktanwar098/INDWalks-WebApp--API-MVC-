using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace INDWalks.API.Data
{
    public class INDWalksAuthDbContext : IdentityDbContext
    {
        public INDWalksAuthDbContext(DbContextOptions<INDWalksAuthDbContext> options) : base(options)
        {
        }
    }
}

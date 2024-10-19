using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Data.Data
{
    public class QuantaDbContext : IdentityDbContext
    {
        public DbSet<Post> Posts { get; set; }
        
        public QuantaDbContext(DbContextOptions options) : base(options) { }
        public QuantaDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Quanta;User Id=SA;Password=SQL1107$;TrustServerCertificate=True;");
        }
    }
}
using FootballersCatalog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FootballersCatalog.Models.Data
{
    public class FootballersCatalogDbContext : DbContext
    {
        public FootballersCatalogDbContext(DbContextOptions<FootballersCatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Footballer> Footballers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}

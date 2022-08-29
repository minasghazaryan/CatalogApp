using CatalogApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogApp.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Catalog> Catalogs { get; set; }
    }
}

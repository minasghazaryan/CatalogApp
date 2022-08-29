using CatalogApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Catalog> Catalogs { get; set; }
    }
}

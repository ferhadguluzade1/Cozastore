using Microsoft.EntityFrameworkCore;
using Cozastore.Models;

namespace Cozastore.DAL
{
    public class CozastoreDB : DbContext
    {
        public CozastoreDB(DbContextOptions options) : base(options)
        {}
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
    }
}

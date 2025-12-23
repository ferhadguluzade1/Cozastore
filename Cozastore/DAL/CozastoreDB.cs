using Microsoft.EntityFrameworkCore;
using Cozastore.Models;

namespace Cozastore.DAL
{
    public class CozastoreDB : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public CozastoreDB(DbContextOptions options) : base(options)
        { }
    }
}

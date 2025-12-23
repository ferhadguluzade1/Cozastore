using Cozastore.Models.Base;

namespace Cozastore.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        

        public ICollection<Product> Products { get; set; } 
    }
}

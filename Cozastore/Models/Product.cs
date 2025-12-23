using Cozastore.Models.Base;

namespace Cozastore.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public string SKU { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Image> Images { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

using Cozastore.Models.Base;
namespace Cozastore.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}

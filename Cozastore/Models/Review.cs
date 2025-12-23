using Cozastore.Models.Base;

namespace Cozastore.Models
{
    public class Review : BaseEntity
    {
        public string Comment { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cozastore.Models;
namespace Cozastore.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public List<Product> RelatedProducts { get; set; }
    }
}

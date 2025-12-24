using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cozastore.Models;

namespace Cozastore.ViewModels
{
    public class DetailViewModel
    {
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cozastore.Models;
namespace Cozastore.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}

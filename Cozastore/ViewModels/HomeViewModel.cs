using Cozastore.Models;
namespace Cozastore.ViewModels
{
    public class HomeViewModel
    {
        public List<Sliders> Slider { get; set; }
        public List<Product> Products { get; set; }
        public Product product { get; set; }
    }
}

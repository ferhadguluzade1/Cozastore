namespace Cozastore.Models
{
    public class Slider : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ButtonText { get; set; }
        public string ButtonLink { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
    }
}

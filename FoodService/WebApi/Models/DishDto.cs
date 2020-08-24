namespace TravelLine.Food.WebApi.Models
{
    public class DishDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public decimal Weight { get; set; }
        public string ImagePath { get; set; }
    }
}

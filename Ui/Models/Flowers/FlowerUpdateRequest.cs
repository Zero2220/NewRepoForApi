namespace Ui.Models.Flowers
{
    public class FlowerUpdateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<int> ImageIds { get; set; }
    }
}

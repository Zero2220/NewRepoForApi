﻿namespace Ui.Models.Flowers
{
    public class FlowerGetRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}

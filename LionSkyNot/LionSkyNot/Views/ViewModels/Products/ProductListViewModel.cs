﻿namespace LionSkyNot.Views.ViewModels.Products
{
    public class ProductListViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public string ImageUrl { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public decimal PriceOnPromotion { get; set; }

        public int InStock { get; set; }

        public bool IsOnPromotion { get; set; }

    }
}

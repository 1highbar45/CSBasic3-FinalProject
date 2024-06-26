﻿namespace FinalProject.Domain.Models.Products
{
	public class HotDealViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
    }
}

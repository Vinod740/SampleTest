namespace ShoppingCart.Models
{
    public class ItemMaster
    {
        public int ItemId { get; set; }
        required
        public string ItemName { get; set; }
        public IFormFile? Image { get; set; }

        public string? ImagePath { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }

        public decimal? Total { get; set; }
        public decimal? GrandTotal { get; set; }
        public int StockSold { get; set; }

    }
}

namespace PriceRater.DataAccess.DTO
{
    public class ProductDTO
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string WebAddress { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public int RetailerId { get; set; }
        public int WebScrapingId { get; set; }
    }
}

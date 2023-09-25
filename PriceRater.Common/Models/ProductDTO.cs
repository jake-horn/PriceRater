namespace PriceRater.Common.Models
{
    public class ProductDTO
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal? ClubcardPrice { get; set; }
        public string WebAddress { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public int RetailerId { get; set; }
        public int? WebScrapingId { get; set; } = null;
    }
}

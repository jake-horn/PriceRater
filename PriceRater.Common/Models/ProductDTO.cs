using System.Text.Json.Serialization;

namespace PriceRater.Common.Models
{
    public class ProductDTO
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("price")]
        public decimal? Price { get; set; }

        [JsonPropertyName("clubcardPrice")]
        public decimal? ClubcardPrice { get; set; }

        [JsonPropertyName("webAddress")]
        public string WebAddress { get; set; }

        [JsonPropertyName("dataAdded")]
        public DateTime DateAdded { get; set; }

        [JsonPropertyName("DateUpdated")]
        public DateTime DateUpdated { get; set; }

        [JsonPropertyName("retailerId")]
        public int RetailerId { get; set; }
    }
}

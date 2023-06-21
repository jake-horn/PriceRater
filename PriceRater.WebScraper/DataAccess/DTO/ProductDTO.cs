using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.WebScraper.DataAccess.DTO
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.WebScraper.Models
{
    public class ProductData
    {
        public string Title { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public decimal? ClubcardPrice { get; set; } = null;
    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.WebScraper.Utilities.Helpers
{
    public static class ScraperHelpers
    {
        /// <summary>
        /// Functionality for returning the decimal value of the price scraped from the website. 
        /// </summary>
        /// <param name="price">An IWebElement consisting of the scraped price</param>
        /// <returns></returns>
        public static decimal ReturnDecimalPriceFromString(IWebElement price)
        {
            string priceTrimmed = new(String.Concat(price.Text.Where(x => x == '.' || Char.IsDigit(x))));
            return Decimal.Parse(priceTrimmed);
        }

        /// <summary>
        /// Functionality for returning the decimal value of the price scraped from the website. 
        /// </summary>
        /// <param name="price">An IWebElement consisting of the scraped price</param>
        /// <returns></returns>
        public static decimal ReturnDecimalPriceFromString(string price)
        {
            string priceTrimmed = new(String.Concat(price.Where(x => x == '.' || Char.IsDigit(x))));
            return Decimal.Parse(priceTrimmed);
        }
    }
}

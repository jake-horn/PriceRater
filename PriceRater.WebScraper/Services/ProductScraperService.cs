using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using PriceRater.WebScraper.Interfaces;
using PriceRater.WebScraper.Models;

namespace PriceRater.WebScraper.Services
{
    public class ProductScraperService : IProductScraperService
    {
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait;

        public ProductScraperService(IWebDriver webDriver, WebDriverWait webDriverWait)
        {
            _webDriver = webDriver;
            _webDriverWait = webDriverWait;
        }

        /// <summary>
        /// Provides a ProductData of the scraped website data. 
        /// </summary>
        /// <param name="retailerConfig">IConfiguration of the retailer</param>
        /// <param name="webAddress">Product web address</param>
        public ProductData ProvideProductData(IConfiguration retailerConfig, string webAddress)
        {
            return ExtractProductData(retailerConfig, webAddress);
        }

        /// <summary>
        /// Scrapes the data from the web address provided.  
        /// </summary>
        /// <param name="retailerConfig">IConfiguration of the retailer</param>
        /// <param name="webAddress">Product web address</param>
        private ProductData ExtractProductData(IConfiguration retailerConfig, string webAddress)
        {
            CookiePopupHandler(retailerConfig, webAddress);

            var titleElement = retailerConfig.GetValue<string>("titleElement");
            var priceElement = retailerConfig.GetValue<string>("priceElement");

            var title = _webDriver.FindElement(By.CssSelector(titleElement));
            var price = _webDriver.FindElement(By.CssSelector(priceElement));

            string priceTrimmed = new(String.Concat(price.Text.Where(x => x == '.' || Char.IsDigit(x))));
            decimal priceDecimal = Decimal.Parse(priceTrimmed);

            return new ProductData()
            {
                Title = title.Text,
                Price = priceDecimal
            };
        }

        /// <summary>
        /// Deals with the cookie pop ups on retailer websites. 
        /// </summary>
        /// <param name="retailerConfig">IConfiguration of the retailer</param>
        /// <param name="webAddress">Product web address</param>
        private void CookiePopupHandler(IConfiguration retailerConfig, string webAddress)
        {
            var cookiesPopUp = retailerConfig.GetValue<string>("cookiesPopUp");

            _webDriver.Navigate().GoToUrl(webAddress);
            IList<IWebElement> elementList = _webDriver.FindElements(By.Id(cookiesPopUp));

            if (elementList.Count > 0)
            {
                _webDriver.FindElement(By.Id(cookiesPopUp)).Click();
            }

            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            _webDriverWait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}

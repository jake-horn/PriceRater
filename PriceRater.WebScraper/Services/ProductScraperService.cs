using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using PriceRater.WebScraper.Interfaces;
using PriceRater.WebScraper.Models;
using Microsoft.Extensions.Logging;
using PriceRater.WebScraper.Utilities.Helpers;

namespace PriceRater.WebScraper.Services
{
    public class ProductScraperService : IProductScraperService
    {
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait;
        private readonly ILogger<IProductScraperService> _logger;

        public ProductScraperService(IWebDriver webDriver, WebDriverWait webDriverWait, ILogger<IProductScraperService> logger)
        {
            _webDriver = webDriver;
            _webDriverWait = webDriverWait;
            _logger = logger;
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
            var productData = new ProductData();

            CookiePopupHandler(retailerConfig, webAddress);

            var titleElement = retailerConfig.GetValue<string>("titleElement")!;
            var priceElement = retailerConfig.GetValue<string>("priceElement")!;

            if (webAddress.Contains("tesco.com"))
            {
                try
                {
                    var clubcardElement = retailerConfig.GetValue<string>("clubcardPriceElement")!;
                    var clubcardPriceDecimal = ScraperHelpers.ReturnDecimalPriceFromString(GetElementTextByCssSelector(clubcardElement));
                    productData.ClubcardPrice = clubcardPriceDecimal;
                }
                catch
                {
                    _logger.LogInformation("No Clubcard price");
                }
            }

            productData.Price = ScraperHelpers.ReturnDecimalPriceFromString(GetElementTextByCssSelector(priceElement));
            productData.Title = GetElementTextByCssSelector(titleElement).Text;

            return productData;
        }

        /// <summary>
        /// Helper method to return the element text from a selector
        /// </summary>
        private IWebElement GetElementTextByCssSelector(string selectorText)
        {
            return _webDriver.FindElement(By.CssSelector(selectorText));
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

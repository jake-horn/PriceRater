using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PriceRater.DataAccess.DTO;
using PriceRater.WebScraper.Models;
using PriceRater.WebScraper.Utilities.Exceptions; 

namespace PriceRater.WebScraper.Services
{
    public class DataScraper : IDataScraper
    {
        private readonly IWebDriver _webDriver;
        private readonly WebDriverWait _webDriverWait;
        private readonly IRetailerConfigurationProvider _retailerConfigurationProvider;

        public DataScraper(IWebDriver webDriver, WebDriverWait webDriverWait, IRetailerConfigurationProvider retailerConfigurationProvider)
        {
            _webDriver = webDriver;
            _webDriverWait = webDriverWait;
            _retailerConfigurationProvider = retailerConfigurationProvider;
        }

        public ProductDTO? ScrapeProductData(int webScraperId, string webAddress)
        {
            var retailerConfig = _retailerConfigurationProvider.GetRetailerConfiguration(webAddress)!;

            if (retailerConfig.Equals("Invalid"))
                throw new RetailerConfigurationException($"Invalid retailer for {webAddress}");

            try
            {
                CookiePopupHandler(retailerConfig, webAddress);

                var productData = ExtractProductData(retailerConfig);

                return new ProductDTO()
                {
                    Title = productData.Title,
                    Price = productData.Price,
                    WebAddress = webAddress,
                    DateAdded = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    RetailerId = int.Parse(retailerConfig.GetValue<string>("retailerId")!),
                    WebScrapingId = webScraperId
                };
            }
            catch (RetailerConfigurationException ex)
            {
                Console.WriteLine($"Failed to add {webAddress}, error: {ex.GetType().FullName} : {ex.Message}");
                return null; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add {webAddress}, error: {ex.GetType().FullName} : {ex.Message}");
                return null;
            }
        }

        private ProductData ExtractProductData(IConfiguration retailerConfig)
        {
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

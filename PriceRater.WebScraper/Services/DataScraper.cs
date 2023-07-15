﻿using Microsoft.Extensions.Configuration;
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
            try
            {
                var retailerConfig = _retailerConfigurationProvider.GetRetailerConfiguration(webAddress);

                DealWithCookies(retailerConfig, webAddress);

                var productData = ExtractProductData(retailerConfig);

                _webDriver.Dispose();

                return new ProductDTO()
                {
                    Title = productData.Title,
                    Price = productData.Price,
                    WebAddress = webAddress,
                    DateAdded = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    RetailerId = int.Parse(retailerConfig.GetValue<string>("retailerId")),
                    WebScrapingId = webScraperId
                };
            }
            catch (RetailerConfigurationException ex)
            {
                _webDriver.Dispose();
                Console.WriteLine($"Failed to add {webAddress}, error: {ex.GetType().FullName} : {ex.Message}");
                return null; 
            }
            catch (Exception ex)
            {
                _webDriver.Dispose();
                Console.WriteLine($"Failed to add {webAddress}, error: {ex.GetType().FullName} : {ex.Message}");
                return null;
            }
        }

        private ProductData ExtractProductData(IConfiguration retailerConfig)
        {
            string titleElement = retailerConfig.GetValue<string>("titleElement");
            string priceElement = retailerConfig.GetValue<string>("priceElement");

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

        private void DealWithCookies(IConfiguration retailerConfig, string webAddress)
        {
            string cookiesPopUp = retailerConfig.GetValue<string>("cookiesPopUp");

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

using Microsoft.AspNetCore.Mvc;
using PriceRater.API.Helpers;
using PriceRater.Common.Models;
using PriceRater.DataAccess.Interfaces;
using System.Text;
using System.Text.Json;

namespace PriceRater.API.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IProductHelpers _productHelpers;

        public ProductController(IProductRepository productRepository, IHttpClientFactory httpClientFactory, IProductHelpers productHelpers)
        {
            _productRepository = productRepository;
            _httpClientFactory = httpClientFactory;
            _productHelpers = productHelpers;
        }

        [HttpGet("getproducts")]
        public IActionResult GetProducts()
        {
            var prices = _productRepository.GetProducts();

            return Ok(prices);
        }

        [HttpGet("getcategories")]
        public IActionResult GetCategories(int userId)
        {
            var categories = _productRepository.GetCategoriesAndProducts(userId);

            var groupedCategories = categories.GroupBy(c => c.CategoryName)
                                                         .Select(group => new
                                                         {
                                                             CategoryName = group.Key,
                                                             Products = group.Select(p => new
                                                             {
                                                                 Title = p.Title,
                                                                 Price = p.Price,
                                                                 RetailerName = p.RetailerName
                                                             })
                                                         })
                                                         .ToList();

            return Ok(groupedCategories);
        }

        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProduct(string webAddress)
        {
            if (webAddress is null)
            {
                return BadRequest("web address is null");
            }

            var doesProductExist = _productRepository.DoesProductExist(webAddress);

            if (doesProductExist)
            {
                var lastUpdated = _productRepository.ProductLastUpdated(webAddress);

                if (DateTimeHelpers.TimeBetweenTwoDates(lastUpdated, DateTime.Now).TotalHours <= 24)
                {
                    return Ok("Product already exists and has been updated within the last 24 hours.");
                }
            }

            try
            {
                var productJson = JsonSerializer.Serialize(webAddress);
                var content = new StringContent(productJson, Encoding.UTF8, "application/json");

                var httpClient = _httpClientFactory.CreateClient("WebScraperApi");

                var response = await httpClient.PostAsync("scraper/scrapeproduct", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<ProductDTO>(jsonResponse);

                    _productHelpers.AddOrUpdateProduct(responseData, _productRepository, doesProductExist);

                    return Ok(responseData);
                }
                else
                {
                    return BadRequest("API request failed");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
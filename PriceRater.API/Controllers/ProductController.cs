using Microsoft.AspNetCore.Mvc;
using PriceRater.Common.Models;
using PriceRater.DataAccess.Interfaces;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PriceRater.API.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IHttpClientFactory _httpClientFactory; 

        public ProductController(IProductRepository productRepository, IHttpClientFactory httpClientFactory)
        {
            _productRepository = productRepository;
            _httpClientFactory = httpClientFactory;
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
            try
            {
                var productJson = JsonSerializer.Serialize(webAddress);

                var httpClient = _httpClientFactory.CreateClient("WebScraperApi");

                var content = new StringContent(productJson, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("scraper/scrapeproduct", content);

                if(response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<ProductDTO>(jsonResponse);

                    _productRepository.AddProduct(responseData);

                    return Ok("Product added successfully");
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
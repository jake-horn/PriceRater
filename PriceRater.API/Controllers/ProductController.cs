using Microsoft.AspNetCore.Mvc;
using PriceRater.DataAccess.Interfaces;

namespace PriceRater.API.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository; 
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
    }
}
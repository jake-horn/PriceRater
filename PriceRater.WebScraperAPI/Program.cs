using PriceRater.DataAccess.Interfaces;
using PriceRater.DataAccess.Repositories;
using PriceRater.WebScraper.Interfaces;
using PriceRater.WebScraper.Retailers.Retailers;
using PriceRater.WebScraper.Retailers;
using PriceRater.WebScraper.Services;

namespace PriceRater.WebScraperAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IScraperController, ScraperController>();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IWebAddressProviderService, WebAddressProviderService>();
            builder.Services.AddTransient<IProductScraperService, ProductScraperService>();
            builder.Services.AddTransient<IProductProviderService, ProductProviderService>();
            builder.Services.AddTransient<IRetailerConfigurationProvider, RetailerConfigurationProvider>();

            // Retailers are added here
            builder.Services.AddTransient<IRetailer, AldiRetailer>();
            builder.Services.AddTransient<IRetailer, AsdaRetailer>();
            builder.Services.AddTransient<IRetailer, MorrisonsRetailer>();
            builder.Services.AddTransient<IRetailer, TescoRetailer>();
            builder.Services.AddTransient<IRetailerProvider, RetailerProvider>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
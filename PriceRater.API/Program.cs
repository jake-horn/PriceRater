using PriceRater.DataAccess;
using PriceRater.DataAccess.Interfaces;
using PriceRater.DataAccess.Repositories;
using Microsoft.Extensions.Hosting;
using PriceRater.API.Authentication.Helpers;
using Microsoft.AspNetCore.Cors.Infrastructure;
using PriceRater.API.Helpers;

namespace PriceRater.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var apiCorsPolicy = "apiCorsPolicy";

            var sqlConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: apiCorsPolicy,
                    policy =>
                    {
                        /*policy.WithOrigins(new[] { "http://localhost:5500", "https://localhost:5500" });
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                        policy.AllowCredentials();*/
                        policy.AllowAnyOrigin();
                    });
            });
          
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient("WebScraperApi", client =>
            {
                client.BaseAddress = new Uri("http://127.0.0.1:5000/");
            });

            builder.Services.AddSingleton<IDbConnectionFactory>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return new SqlConnectionFactory(configuration);
            });

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IProductHelpers, ProductHelpers>();

            var app = builder.Build();

            app.UseCors(apiCorsPolicy);

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
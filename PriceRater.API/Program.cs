using PriceRater.DataAccess;
using PriceRater.DataAccess.Interfaces;
using PriceRater.DataAccess.Repositories;
using Microsoft.Extensions.Hosting;
using PriceRater.API.Authentication.Helpers;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace PriceRater.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var apiCorsPolicy = "_apiCorsPolicy";

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
                        policy.WithOrigins(new[] { "https://localhost:3000", "https://localhost:8080", "https://localhost:4200" });
                        policy.AllowAnyHeader();
                        policy.AllowAnyMethod();
                        policy.AllowCredentials();
                    });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IDbConnectionFactory>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return new SqlConnectionFactory(configuration);
            });

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IJwtService, JwtService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(apiCorsPolicy);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
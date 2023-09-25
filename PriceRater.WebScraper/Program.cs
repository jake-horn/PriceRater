﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceRater.WebScraper;
using Microsoft.Extensions.Configuration;
using PriceRater.WebScraper.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PriceRater.DataAccess.Interfaces;
using PriceRater.DataAccess.Repositories;
using PriceRater.DataAccess;
using PriceRater.WebScraper.Utilities.Settings;
using PriceRater.WebScraper.Interfaces;
using PriceRater.WebScraper.Retailers.Retailers;
using PriceRater.WebScraper.Retailers;
using Microsoft.AspNetCore.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((hostingContext, services) =>
    {
        services.AddSingleton<Run>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IProductScraperService, ProductScraperService>();
        services.AddTransient<IProductProviderService, ProductProviderService>();
        services.AddTransient<IScraperController, ScraperController>();

        // Retailers are added here
        services.AddTransient<IRetailer, AldiRetailer>();
        services.AddTransient<IRetailer, AsdaRetailer>();
        services.AddTransient<IRetailer, MorrisonsRetailer>();
        services.AddTransient<IRetailer, TescoRetailer>();
        services.AddTransient<IRetailerProvider, RetailerProvider>();

        /*services.AddSingleton<IWebDriver>(provider =>
        {
            return new ChromeDriver();
        });*/

        services.AddSingleton<IWebDriver, ChromeDriver>();

        services.AddSingleton(provider =>
        {
            var webDriver = provider.GetRequiredService<IWebDriver>();
            return new WebDriverWait(webDriver, TimeSpan.FromSeconds(2));
        });

        services.AddTransient<IRetailerConfigurationProvider, RetailerConfigurationProvider>();
        services.AddSingleton<IDbConnectionFactory>(provider =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            return new SqlConnectionFactory(configuration);
        });

        // Sets up the configuration for the solution root
        var configuration = hostingContext.Configuration;
        var solutionRootConfiguration = configuration.GetSection("SolutionRoot").Value;
        services.AddSingleton(new SolutionRootModel { SolutionRoot = solutionRootConfiguration });
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
        webBuilder.UseUrls("http://127.0.0.1:5000");
    })
    .Build();

var run = host.Services.GetRequiredService<Run>();

//run.ExecuteProgram();

await host.RunAsync(); 
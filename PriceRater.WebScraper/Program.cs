using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceRater.WebScraper;
using Microsoft.Extensions.Configuration;
using PriceRater.WebScraper.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PriceRater.WebScraper.Utilities.Settings;
using PriceRater.WebScraper.Interfaces;
using PriceRater.WebScraper.Retailers.Retailers;
using PriceRater.WebScraper.Retailers;
using Microsoft.AspNetCore.Hosting;
using Serilog;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((hostingContext, services) =>
    {
        // Set up any locations from appsettings.json that are required
        var configuration = hostingContext.Configuration;
        var solutionRootConfiguration = configuration.GetSection("SolutionRoot").Value;
        var loggingConfigLocation = configuration.GetSection("LoggingLocation").Value;

        services.AddLogging(builder =>
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(loggingConfigLocation!, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.AddSerilog(logger);
        });


        services.AddTransient<IProductScraperService, ProductScraperService>();
        services.AddTransient<IProductProviderService, ProductProviderService>();
        services.AddTransient<IScraperController, ScraperController>();

        // Retailers are added here
        services.AddTransient<IRetailer, AldiRetailer>();
        services.AddTransient<IRetailer, AsdaRetailer>();
        services.AddTransient<IRetailer, MorrisonsRetailer>();
        services.AddTransient<IRetailer, TescoRetailer>();
        services.AddTransient<IRetailerProvider, RetailerProvider>();

        services.AddScoped<IWebDriver, ChromeDriver>();

        services.AddScoped(provider =>
        {
            var webDriver = provider.GetRequiredService<IWebDriver>();
            return new WebDriverWait(webDriver, TimeSpan.FromSeconds(2));
        });

        services.AddTransient<IRetailerConfigurationProvider, RetailerConfigurationProvider>();

        // Sets up the configuration for the solution root
        services.AddSingleton(new SolutionRootModel { SolutionRoot = solutionRootConfiguration });

    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
        webBuilder.UseUrls("http://127.0.0.1:5000");
    })
    .Build();

await host.RunAsync(); 
using Microsoft.Extensions.DependencyInjection;
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
        services.AddTransient<IWebAddressProviderService, WebAddressProviderService>();
        services.AddTransient<IDataScraper, DataScraper>();

        services.AddSingleton<IWebDriver>(provider =>
        {
            return new ChromeDriver();
        });

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
    .Build();

var run = host.Services.GetRequiredService<Run>();

run.StartProgram(); 

await host.RunAsync(); 
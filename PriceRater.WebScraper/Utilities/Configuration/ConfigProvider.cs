using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.WebScraper.Utilities.Configuration
{
    public static class ConfigProvider
    {
        /// <summary>
        /// Returns the configuration for the retailer depending on the file path provided in the parameter
        /// </summary>
        /// <param name="solutionRoot">Root of the solution, to locate the json file</param>
        /// <param name="jsonFilePath">File path for the json configuration</param>
        /// <returns></returns>
        public static IConfiguration GetConfiguration(string solutionRoot, string jsonFilePath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(solutionRoot)
                .AddJsonFile(jsonFilePath, optional: false, reloadOnChange: true)
                .Build();
        }
    }
}

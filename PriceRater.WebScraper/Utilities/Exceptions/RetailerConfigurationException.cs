using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.WebScraper.Utilities.Exceptions
{
    public class RetailerConfigurationException : Exception
    {
        public RetailerConfigurationException(string message) : base(message)
        {

        }
    }
}

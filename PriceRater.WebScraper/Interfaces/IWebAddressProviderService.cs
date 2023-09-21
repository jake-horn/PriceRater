﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.WebScraper.Interfaces
{
    public interface IWebAddressProviderService
    {
        public IDictionary<int, string> GetWebAddresses();
    }
}
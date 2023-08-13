using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.DataAccess.DTO
{
    public class UserCategoryDTO
    {
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string RetailerName { get; set; }
    }
}

using PriceRater.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceRater.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        User Create(User user);

        User GetUserByEmail(string email);

        User GetUserById(int id);
    }
}

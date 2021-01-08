using MobileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
    }
}

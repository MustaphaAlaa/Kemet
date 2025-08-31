using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Kemet.Application.Interfaces.Services.Tokens
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}

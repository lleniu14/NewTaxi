using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.Users;

namespace WebApi.Services
{
    public interface ITokenService
    {
        Task<TokenDto> CreateToken(AuthenticateModel model);
    }
}

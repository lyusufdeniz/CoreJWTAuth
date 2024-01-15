using Auth.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Service
{
    public interface ITokenService
    {
        TokenDTO CreateToken(User user);
        ClientTokenDTO CreateClientToken(Client client);
    }
}

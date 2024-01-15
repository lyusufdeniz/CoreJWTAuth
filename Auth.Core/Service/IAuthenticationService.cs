using Auth.Core.Entities;
using SharedLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Service
{
    public interface IAuthenticationService
    {
        Task<ResponseDTO<TokenDTO>> CreateTokenAsync(LoginDTO user);
        Task<ResponseDTO<TokenDTO>> CreateTokenByRefreshTokenAsync(string refreshToken);
        Task<ResponseDTO<NoContentDTO>> RevokeRefreshTokenAsync(string refreshtoken);
        Task<ResponseDTO<ClientTokenDTO>> CreateClientTokenAsync(ClientLoginDTO clientLoginDTO);
    }
}

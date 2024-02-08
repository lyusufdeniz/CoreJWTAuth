using Auth.Core.Entities;
using Auth.Core.Service;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTO;
using System.Runtime.CompilerServices;

namespace Auth.JWT.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDTO loginDTO)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDTO);
            return ActionResultInstance(result);

        }
        [HttpPost]
        public async Task<IActionResult> CreateTokenByClient(ClientLoginDTO clientLoginDTO)
        {
            var result=await _authenticationService.CreateClientTokenAsync(clientLoginDTO);
            return ActionResultInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDTO refreshtoken)
        {
           var resutl= await _authenticationService.RevokeRefreshTokenAsync(refreshtoken.RefreshToken);
            return ActionResultInstance(resutl);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDTO refreshtoken)
        {
            var result=await _authenticationService.CreateTokenByRefreshTokenAsync(refreshtoken.RefreshToken);
            return ActionResultInstance(result);
        }
    }
}

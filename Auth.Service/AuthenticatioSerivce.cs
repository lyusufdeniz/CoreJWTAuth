using Auth.Core.Entities;
using Auth.Core.Repository;
using Auth.Core.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.DTO;

namespace Auth.Service
{
    public class AuthenticatioSerivce : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _repositoryRefreshToken;

        public AuthenticatioSerivce(IOptions<List<Client>> clients, ITokenService tokenService, UserManager<User> userService, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> repositoryRefreshToken)
        {
            _clients = clients.Value;
            _tokenService = tokenService;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _repositoryRefreshToken = repositoryRefreshToken;
        }

        public async Task<ResponseDTO<ClientTokenDTO>> CreateClientTokenAsync(ClientLoginDTO clientLoginDTO)
        {

            var client =_clients.SingleOrDefault(x=>x.ClientId == clientLoginDTO.ClientId&&x.Secret==clientLoginDTO.ClientSecret);
            if (client == null)
            {
                return ResponseDTO<ClientTokenDTO>.Fail(400, "Client Not Found", true);

            }
            var token = _tokenService.CreateClientToken(client);
            return ResponseDTO<ClientTokenDTO>.Succes(token, 200);


        }

        public async Task<ResponseDTO<TokenDTO>> CreateTokenAsync(LoginDTO loginuser)
        {
            if (loginuser == null) throw new ArgumentNullException(nameof(loginuser));
            var user = await _userService.FindByEmailAsync(loginuser.Email);
            if (user == null) return ResponseDTO<TokenDTO>.Fail(400, "Email or password wrong", true);
            if (!await _userService.CheckPasswordAsync(user, loginuser.Password)) return ResponseDTO<TokenDTO>.Fail(400, "Email or password wrong", true);

            var token = _tokenService.CreateToken(user);
            var userRefreshToken = await _repositoryRefreshToken.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
            if (userRefreshToken == null)
            {
                await _repositoryRefreshToken.AddAsync(new UserRefreshToken { UserId = user.Id, Token = token.RefreshToken, Expiration = token.RefreshExpire });

            }
            else
            {
                userRefreshToken.Token = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshExpire;

            }

            await _unitOfWork.CommitAsync();
            return ResponseDTO<TokenDTO>.Succes(token,200);
        }

        public async Task<ResponseDTO<TokenDTO>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var existrefreshtoken= await _repositoryRefreshToken.Where(x=>x.Token == refreshToken).SingleOrDefaultAsync();
            if (existrefreshtoken == null)
                return ResponseDTO<TokenDTO>.Fail(404, "Refresh Not Found",true);

            var user = await _userService.FindByIdAsync(existrefreshtoken.UserId);
            if (user == null)   return ResponseDTO<TokenDTO>.Fail(404, "user Not Found", true);
            var token= _tokenService.CreateToken(user);
            existrefreshtoken.Token=token.RefreshToken;
            existrefreshtoken.Expiration=token.RefreshExpire;
            await _unitOfWork.CommitAsync();
            return ResponseDTO<TokenDTO>.Succes(token, 200);
        }

        public async Task<ResponseDTO<NoContentDTO>> RevokeRefreshTokenAsync(string refreshtoken)
        {
           var existrefreshtoken=await _repositoryRefreshToken.Where(x=>x.Token==refreshtoken).SingleOrDefaultAsync();
            if (existrefreshtoken == null)
                return ResponseDTO<NoContentDTO>.Fail(404, "Refresh Not Found", true);
            _repositoryRefreshToken.Remove(existrefreshtoken);
            await _unitOfWork.CommitAsync();
            return ResponseDTO<NoContentDTO>.Succes(200);
            
        }
    }
}

using Auth.Core.Entities;
using Auth.Core.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;




namespace Auth.Service
{
    public class TokenService : ITokenService
    {

        private readonly CustomTokenOptions _customTokenOptions;

        public TokenService( IOptions<CustomTokenOptions> customTokenOptions)
        {

            _customTokenOptions = customTokenOptions.Value;
        }
        private string CreateRefresherToken()
        {
            var numberbyte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberbyte);
            return Convert.ToBase64String(numberbyte);
        }
        private IEnumerable<Claim> GetClaims(User user, List<string> Audiences)
        {
            var userlist = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()) };
            userlist.AddRange(Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userlist;

        }
        private IEnumerable<Claim> GetClaimByClient(Client client)
        {

            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, client.ClientId.ToString()));
            return claims;
        }

        public ClientTokenDTO CreateClientToken(Client client)
        {
            var AccesTokenExpiration = DateTime.Now.AddMinutes(_customTokenOptions.AccesTokenExpiration);

            var SecurityKey = SignService.GetSymmetricSecurityKey(_customTokenOptions.SecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: _customTokenOptions.Issuer,
                expires: AccesTokenExpiration, 
                notBefore: DateTime.Now, 
                claims: GetClaimByClient(client),
                signingCredentials: signingCredentials);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            return new ClientTokenDTO { Token = token, Expire = AccesTokenExpiration };

        }

        public TokenDTO CreateToken(User user)
        {
            var AccesTokenExpiration = DateTime.Now.AddMinutes(_customTokenOptions.AccesTokenExpiration);
            var RefreshTokenExpiration = DateTime.Now.AddMinutes(_customTokenOptions.RefreshTokenExpiration);
            var SecurityKey = SignService.GetSymmetricSecurityKey(_customTokenOptions.SecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: _customTokenOptions.Issuer, expires: AccesTokenExpiration, notBefore: DateTime.Now, claims: GetClaims(user, _customTokenOptions.Audience),
                signingCredentials: signingCredentials);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.WriteToken(jwtSecurityToken);

            return new TokenDTO { AccessToken = token, RefreshToken = CreateRefresherToken(), AccessExpire = AccesTokenExpiration, RefreshExpire = RefreshTokenExpiration };
        }
    }
}

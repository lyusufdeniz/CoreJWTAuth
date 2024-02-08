
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Auth.Service
{
    public static class SignService
    {
        public static SecurityKey GetSymmetricSecurityKey (string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
    }
}

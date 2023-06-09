using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace JWTrix.App.Helpers
{
    public static class JwtHelper
    {
        public static bool CheckIsJwt(string? text)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.CanReadToken(text);
        }

        public static JwtSecurityToken ConvertToJwtSecurityToken(string content)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(content);
        }
    }
}

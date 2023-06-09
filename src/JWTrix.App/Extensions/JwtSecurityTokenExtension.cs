using JWTrix.App.Helpers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTrix.App.Extensions
{
    public static class JwtSecurityTokenExtension
    {
        public static string DecodeHeader(this JwtSecurityToken? jwtSecurityToken, bool writeIndented = true)
        {
            ArgumentNullException.ThrowIfNull(jwtSecurityToken);

            var header = Base64UrlEncoder.Decode(jwtSecurityToken.RawHeader);
            return writeIndented ? JsonHelper.Beautify(header) : header;
        }

        public static string DecodePayload(this JwtSecurityToken? jwtSecurityToken, bool writeIndented = true)
        {
            ArgumentNullException.ThrowIfNull(jwtSecurityToken);

            var header = Base64UrlEncoder.Decode(jwtSecurityToken.RawPayload);
            return writeIndented ? JsonHelper.Beautify(header) : header;
        }
    }
}

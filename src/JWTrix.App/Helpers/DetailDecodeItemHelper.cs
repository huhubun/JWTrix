using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace JWTrix.App.Helpers
{
    public static class DetailDecodeItemHelper
    {
        private static readonly IReadOnlySet<string> _dateTimeItems = new HashSet<string> {
            JwtRegisteredClaimNames.Exp,
            JwtRegisteredClaimNames.Nbf,
            JwtRegisteredClaimNames.Iat
        };

        public static IReadOnlySet<string> DateTimeItems => _dateTimeItems;
    }
}

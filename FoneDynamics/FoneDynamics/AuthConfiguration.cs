using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoneDynamics
{
    public class AuthConfiguration
    {
        public const string ValidIssuer = "suchvalid.issuer";
        public const string ValidAudience = "suchvalid.clients";
        public static DateTime JwtTokenExpiration { get => DateTime.Now.AddHours(1); }
        public static SymmetricSecurityKey SymmetricSecurityKey
        {
            get
            {
                //security key
                string securityKey = "this is our super long security key for token validation project";

                //symmetric security key(
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

                return symmetricSecurityKey;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Identity
{
    public class TokenResponse
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsVerified { get; set; }
        public string JWToken { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
       
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}

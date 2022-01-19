using Application.DTOs.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Infrastructure.Identity.Models
{
    public  class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool IsActive { get; set; } = false;

        public List<RefreshToken> RefreshTokens { get; set; }
    }
}

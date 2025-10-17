using Microsoft.AspNetCore.Identity;

namespace LinkUp.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string ProfileImage { get; set; }
        public required bool IsActive { get; set; }
    }
}


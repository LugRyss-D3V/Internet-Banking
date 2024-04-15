
using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PersonalId { get; set; }
        public bool Status { get; set; }
    }
}

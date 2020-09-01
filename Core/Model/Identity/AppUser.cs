using Microsoft.AspNetCore.Identity;

namespace Core.Model.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public Address Address {get; set;}
    }
}
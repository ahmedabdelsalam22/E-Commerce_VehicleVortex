using Microsoft.AspNetCore.Identity;

namespace VehicleVortex.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}

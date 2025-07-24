using Microsoft.AspNetCore.Identity;

namespace MagicVilla_API;

public class ApplicationUser:IdentityUser
{
    public string Name { get; set; }
}
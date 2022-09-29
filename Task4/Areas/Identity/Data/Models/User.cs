using Microsoft.AspNetCore.Identity;

namespace Task4.Areas.Identity.Data.Models
{
    public class User : IdentityUser
    {
        public string? RegistrationTime { get; set; }
        public string? LoginTime { get; set; }
    }
}
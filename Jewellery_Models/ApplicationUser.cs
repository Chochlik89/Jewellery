using Microsoft.AspNetCore.Identity;

namespace Jewellery_Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace restaurant_app_dotnet_mvc.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order> Orders { get; set; }
    }
}

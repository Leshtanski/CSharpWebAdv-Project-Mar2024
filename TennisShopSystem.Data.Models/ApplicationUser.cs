namespace TennisShopSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// This is a custom user class that works with the default ASP.Net Core Identity.
    /// You can add more info to the built-in users.
    /// </summary>

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.BoughtProducts = new HashSet<Product>();
        }

        public virtual ICollection<Product> BoughtProducts { get; set; }
    }
}

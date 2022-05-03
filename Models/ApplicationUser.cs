using Microsoft.AspNetCore.Identity;

namespace ProjektDotnet.Models;
public class ApplicationUser : IdentityUser
{
    public ICollection<Favourites> Favourites { get; set; }
    public ICollection<Ratings> Ratings { get; set; }
}

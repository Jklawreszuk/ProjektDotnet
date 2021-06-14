using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDotnet.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Favourites> Favourites { get; set; }
        public ICollection<Ratings> Ratings { get; set; }
    }
}

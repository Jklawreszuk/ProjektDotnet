using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektDotnet.Models
{
    public class Favourites
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}

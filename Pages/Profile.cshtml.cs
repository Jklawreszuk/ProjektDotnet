using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjektDotnet.Data;
using ProjektDotnet.Models;

namespace ProjektDotnet.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ILogger<ProfileModel> _logger;
        private readonly ApplicationDbContext _context;

        public string UserName { get; set; }
        public IEnumerable<Recipe> Recipes { get; set; }
        public ProfileModel(ILogger<ProfileModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet(string username)
        {
            UserName = username;
            Recipes = _context.Recipe.
            Where(r => r.User.UserName == username).
            OrderByDescending(d => d.Date);
        }
    }
}

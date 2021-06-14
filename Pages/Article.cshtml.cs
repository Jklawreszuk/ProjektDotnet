using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProjektDotnet.Data;
using ProjektDotnet.Models;

namespace ProjektDotnet.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly ILogger<ArticleModel> _logger;
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public Recipe Recipe { get; set; }
        public List<RecipeCategory> Category { get; set; }

        public List<Ingredient> Ingredients { get; set; }
        public List<Images> Images {get;set;}

        [BindProperty]
        public int RowId { get; set; }

        public ArticleModel(ILogger<ArticleModel> logger,ApplicationDbContext context, UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }

        public void OnGet(int id)
        {
           Recipe = _context.Recipe.Where(r=>r.Id==id).First();
           Category = _context.RecipeCategory.Where(r=>r.RecipeId==id).ToList();
           Ingredients = _context.Ingredient.Where(r=>r.RecipeId==id).ToList();
           Images = _context.Images.Where(i=>i.RecipeId==id).ToList();

        }

        public IActionResult OnPost()
        {
            var favourites = new Favourites()
            {
                RecipeId = RowId,
                User = _userManager.FindByNameAsync(User.Identity.Name).Result
            };

            _applicationDbContext.Add(favourites);
            _applicationDbContext.SaveChanges();
            return RedirectToPage();
        }
    }
}

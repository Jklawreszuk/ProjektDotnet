using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Recipe Recipe { get; set; }
        public List<RecipeCategory> Category { get; set; }

        public List<Ingredient> Ingredients { get; set; }
        public List<Images> Images {get;set;}

        public ArticleModel(ILogger<ArticleModel> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet(int id)
        {
           Recipe = _context.Recipe.Where(r=>r.Id==id).First();
           Category = _context.RecipeCategory.Where(r=>r.RecipeId==id).ToList();
           Ingredients = _context.Ingredient.Where(r=>r.RecipeId==id).ToList();
           Images = _context.Images.Where(i=>i.RecipeId==id).ToList();

        }
    }
}

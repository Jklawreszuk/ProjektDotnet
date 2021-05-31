using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjektDotnet.Models;
using ProjektDotnet.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjektDotnet.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Recipe> Recipe { get; set; }
        public SelectList Category { get; set; }
        public string SearchCategory { get; set; }

        public List<Recipe> RecipeCategory { get; set; } //lista #2 na te przepisy z konkretną kategorią

        public async Task OnGetAsync(string searchCategory, string searchString)
        {
            IQueryable<string> categoryQuery = from c in _context.Category orderby c.Name select c.Name;

            searchString = searchString ?? string.Empty;
            Recipe = _context.Recipe.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())).ToList();

            searchCategory = searchCategory ?? string.Empty;
          var RecipeCategory = /*_context.Recipe.*/(from r in _context.Recipe
                                            join rc in _context.RecipeCategory on r.Id equals rc.RecipeId
                                            join c in _context.Category on rc.CategoryId equals c.Id
                                            where c.Name == searchCategory
                                            select r
            ).ToList();

            /*.Select(r => new Recipe
         {

             RecipeCategories = r.RecipeCategories
             .Where(rc => rc.Category.Name.Contains(searchCategory)).ToList()
         }).ToList();*/


            /*.Include(r => r.RecipeCategories).ThenInclude(rc => rc.Category)
        .Where(c => c.Name.Contains(searchCategory)).ToList();*/


            /*(from r in _context.Recipe
              join rc in _context.RecipeCategory on r.Id equals rc.RecipeId
              join c in _context.Category on rc.CategoryId equals c.Id
              where c.Name == searchCategory
              select new
              {
                  Recipe = r
              }
            ).ToList();*/


            //.Where(c => c.Name.Contains(searchCategory)).ToList();


            /*var temp = _context.Recipe.Select(s => new
            {
                Recipe = s,
                RecipeCategories = s.RecipeCategories
                .Where(e => e.Category.Name.Contains("Obiad")).ToList()
            }).ToList();*/



            Category = new SelectList(await categoryQuery.ToListAsync());
           
               
        }
    }
}

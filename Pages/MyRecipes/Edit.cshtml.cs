using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektDotnet.Data;
using ProjektDotnet.Models;

namespace ProjektDotnet.Pages.MyRecipes
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RecipeViewModel RecipeViewModel { get; set; }
        public Recipe Recipe { get; set; }
        public List<Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recipe = await _context.Recipe
                .Include("User").Include("RecipeCategories.Category").Include("Ingredients").FirstOrDefaultAsync(m => m.Id == id);

            var ing = Recipe?.Ingredients?.Select(p=>p.Name)?.ToArray();
            var cat = Recipe?.RecipeCategories?.Select(p=>p.Category?.Name)?.ToArray();
            RecipeViewModel = new RecipeViewModel()
            {

                Name = Recipe.Name,
                Description = Recipe.Description,
                Ingredients = ing,
                Categories =  cat, 
                // ProfileImages = Recipe.Images.ToArray()

            };
            Categories = await _context.Category.ToListAsync();



            if (Recipe == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var recipe = new Recipe()
            {
                Id = Recipe.Id,
                Name = RecipeViewModel.Name,
                Description = RecipeViewModel.Description,
                User = Recipe.User,
                Ingredients = Utilis.GetIngredients(RecipeViewModel.Ingredients).ToList(),
                RecipeCategories = Utilis.GetRecipeCategories(RecipeViewModel.Categories).ToList(),
                Date = DateTime.Now,
                Images = Utilis.UploadedFile(RecipeViewModel).ToList()
            };

            _context.Recipe.Update(recipe);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(Recipe.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.Id == id);
        }
    }
}

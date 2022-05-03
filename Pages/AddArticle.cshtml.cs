using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjektDotnet.Data;
using ProjektDotnet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ProjektDotnet.Pages
{
    [Authorize]
    public class AddArticleModel : PageModel
    {
        private readonly ILogger<AddArticleModel> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public List<Category> Categories { get; set; }

        [BindProperty]
        public RecipeViewModel RecipeViewModel { get; set; }

        public AddArticleModel(ILogger<AddArticleModel> logger, ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet()
        {
            Categories = _applicationDbContext.Category.ToList();
        }


        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var recipe = new Recipe()
                {
                    Name = RecipeViewModel.Name,
                    Description = RecipeViewModel.Description,
                    User = _userManager.FindByNameAsync(User.Identity.Name).Result,
                    Ingredients = Utilis.GetIngredients(RecipeViewModel.Ingredients).ToList(),
                    RecipeCategories = Utilis.GetRecipeCategories(RecipeViewModel.Categories, _applicationDbContext.Category.ToList()).ToList(),
                    Date = DateTime.Now,
                    Images = Utilis.UploadedFile(RecipeViewModel).ToList()
                };
                _applicationDbContext.Add(recipe);
                _applicationDbContext.SaveChanges();
            }

            return RedirectToPage();

        }


    }
}

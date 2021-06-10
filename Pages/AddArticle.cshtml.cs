using System.Collections.Immutable;
using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ProjektDotnet.Data;
using ProjektDotnet.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace ProjektDotnet.Pages
{
    [Authorize]
    public class AddArticleModel : PageModel
    {
        private readonly ILogger<AddArticleModel> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public List<Category> Categories { get; set; }

        [BindProperty]
        public RecipeViewModel RecipeViewModel { get; set; }

        public AddArticleModel(ILogger<AddArticleModel> logger, ApplicationDbContext applicationDbContext, IWebHostEnvironment hostEnvironment, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet()
        {
            Categories = _applicationDbContext.Category.ToList();
        }

        private IEnumerable<Ingredient> GetIngredients(string[] selectList)
        {
            foreach (var item in selectList)
            {
                yield return new Ingredient() { Name = item };
            }

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
                    Ingredients = GetIngredients(RecipeViewModel.Ingredients).ToList(),
                    Date = DateTime.Now,
                    Images = UploadedFile(RecipeViewModel).ToList()
                };
                _applicationDbContext.Add(recipe);
                _applicationDbContext.SaveChanges();
            }

            return RedirectToPage();

        }
        private IEnumerable<Images> UploadedFile(RecipeViewModel model)
        {

            if (model.ProfileImages != null)
            {
                
                foreach (var img in model.ProfileImages)
                {
                    using var memoryStream = new MemoryStream();

                    img.CopyTo(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        var file = new Images() { Image = memoryStream.ToArray() };
                        yield return file;
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                    }
                    


                }

            }

        }
    }
}

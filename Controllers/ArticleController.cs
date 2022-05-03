using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjektDotnet.Data;
using ProjektDotnet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ProjektDotnet.Controllers;
public class ArticleController : Controller
{
    private readonly ILogger<ArticleController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public Recipe Recipe { get; set; }

    public ArticleController(ILogger<ArticleController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    [HttpGet("Article/{id}", Name = "Article")]
    public IActionResult Index(int id)
    {
        Recipe = _context.Recipe.
            Include(r => r.User).
            Include(r => r.Images).
            Include(r => r.Ingredients).
            Where(n => n.Id == id).First();
        return View(Recipe);
    }

    public IActionResult AddToFavourites([FromForm] int RowId)
    {
        var fav = new Favourites()
        {
            RecipeId = RowId,
            User = _userManager.FindByNameAsync(User.Identity.Name).Result
        };

        var result = _context.Favourites.Find(
            _userManager.FindByNameAsync(User.Identity.Name).Result.Id, RowId);

        if (result == null)
        {
            _context.Add(fav);
            _context.SaveChanges();
        }

        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpPost, Authorize]
    public IActionResult Like([FromForm] int RowId)
    {
        var ratings = new Ratings()
        {
            RecipeId = RowId,
            User = _userManager.FindByNameAsync(User.Identity.Name).Result,
            IsLike = true
        };

        var result = _context.Ratings.Find(_userManager.FindByNameAsync(User.Identity.Name).Result.Id, RowId);

        if (result == null)
        {
            _context.Add(ratings);
            Recipe = _context.Recipe
            .Include(r => r.User).
            Include(r => r.Images).
            Include(r => r.Ingredients).
            Where(n => n.Id == RowId).First();

            Recipe.LikeCount++;
            _context.Update(Recipe);
            _context.SaveChanges();
        }

        return Redirect(Request.Headers["Referer"].ToString());

    }
    [HttpPost, Authorize]
    public async Task<IActionResult> Dislike([FromForm] int RowId)
    {
        var ratings = new Ratings()
        {
            RecipeId = RowId,
            User = await _userManager.FindByNameAsync(User.Identity.Name),
            IsLike = false
        };

        var result = _context.Ratings.FindAsync(_userManager.FindByNameAsync(User.Identity.Name).Id, RowId);

        if (result == null)
        {
            Recipe = _context.Recipe.
            Include(r => r.User).
            Include(r => r.Images).
            Include(r => r.Ingredients).
            Where(n => n.Id == RowId).First();
            
            _context.Add(ratings);
            Recipe.DislikeCount ++;
            _context.Update(Recipe);
            _context.SaveChanges();
        }

        return Redirect(Request.Headers["Referer"].ToString());
    }

}
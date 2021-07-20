using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjektDotnet.Data;
using ProjektDotnet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ProjektDotnet.Controllers
{
    [Authorize]
    public class FavouritesController : Controller
    {
        private readonly ILogger<FavouritesController> _logger;
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public List<Favourites> Favourites { get; set; }

        public FavouritesController(ILogger<FavouritesController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            Favourites = _context.Favourites
                .Include(r => r.Recipe).Where(n => n.User.Id == _userManager.FindByNameAsync(User.Identity.Name).Result.Id).ToList();
            return View(Favourites);
        }

        [HttpPost]
        public IActionResult Delete([FromForm] int RowId)
        {
            var fav = _context.Favourites.Where(n =>
            n.User == _userManager.FindByNameAsync(User.Identity.Name).Result
            && n.RecipeId == RowId
            ).First();
            _context.Remove(fav);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Modify([FromForm] int RowId)
        {
            var fav = _context.Favourites.Where(n =>
            n.User == _userManager.FindByNameAsync(User.Identity.Name).Result
            && n.RecipeId == RowId
            );
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }




    }
}
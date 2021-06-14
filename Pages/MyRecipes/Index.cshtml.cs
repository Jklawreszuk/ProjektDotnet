using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektDotnet.Data;
using ProjektDotnet.Models;

namespace ProjektDotnet.Pages.MyRecipes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Recipe> Recipe { get;set; }
        [BindProperty]
        public int Id { get; set; }

        public async Task OnGetAsync()
        {
            Recipe = await _context.Recipe
                .Include(r => r.User).Where(n=>n.UserId==User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var q = _context.Recipe.Where(n=>(n.Id==Id) && (n.UserId==User.FindFirstValue(ClaimTypes.NameIdentifier)));
            
            if(q.Count()>0)
            {
                
                _context.Remove(new Recipe(){Id=Id});
                await _context.SaveChangesAsync();
            }
            
            return RedirectToPage();
        }
    }
}

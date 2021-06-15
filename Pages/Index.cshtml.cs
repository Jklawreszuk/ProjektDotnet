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
        public List<Recipe> Recipes { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync()
        {
             Recipes = await _context.Recipe.OrderByDescending(p=>p.LikeCount).Take(10).ToListAsync();
        }
    }
}

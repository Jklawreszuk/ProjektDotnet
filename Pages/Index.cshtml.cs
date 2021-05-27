using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjektDotnet.Models;

namespace ProjektDotnet.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ProjektDotnet.Data.ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ProjektDotnet.Data.ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public List<Recipe> Recipe { get; set; }
        public async Task OnGetAsync(string searchString)
        {
            
            searchString = searchString ?? string.Empty;
            Recipe = _context.Recipe.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())).ToList();

        }
    }
}

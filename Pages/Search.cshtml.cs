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

namespace ProjektDotnet.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ILogger<SearchModel> _logger;
        private readonly ApplicationDbContext _context;

        public SearchModel(ILogger<SearchModel> logger, ApplicationDbContext context)
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

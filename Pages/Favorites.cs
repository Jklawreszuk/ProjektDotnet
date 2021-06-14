﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjektDotnet.Data;
using ProjektDotnet.Models;

namespace ProjektDotnet.Pages
{
    public class FavoritesModel : PageModel
    {
        private readonly ILogger<FavoritesModel> _logger;
        private readonly ApplicationDbContext _context;
        public List<Recipe> Recipes { get; set; }

        public FavoritesModel(ILogger<FavoritesModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnGetAsync()
        {
             Recipes = await _context.Recipe.ToListAsync();
        }
    }
}
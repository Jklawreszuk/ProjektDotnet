﻿using System;
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
    public class SearchModel : PageModel
    {
        private readonly ILogger<SearchModel> _logger;
        private readonly ApplicationDbContext _context;

        public SearchModel(ILogger<SearchModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

       // public List<Recipe> Recipe { get; set; }
        //public SelectList Category { get; set; }
        public List<Recipe> RecipeCategories { get; set; } 
        

        public async Task OnGetAsync(string searchString, string searchCategory, string searchAuthor)
        {
            searchString = searchString ?? string.Empty;
            // Recipe = await _context.Recipe.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())).OrderByDescending(s => s.Date).ToListAsync();

            //IQueryable<string> categoryQuery = from c in _context.Category orderby c.Name select c.Name;

            searchAuthor = searchAuthor ?? string.Empty;
            searchCategory = searchCategory ?? string.Empty;
            var recipeCategory = (from r in _context.Recipe
                                                  join rc in _context.RecipeCategory on r.Id equals rc.RecipeId
                                                  join c in _context.Category on rc.CategoryId equals c.Id
                                                  join u in _context.Users on r.UserId equals u.Id
                                                  where c.Name.ToUpper().Contains(searchCategory.ToUpper()) 
                                                  && r.Name.ToUpper().Contains(searchString.ToUpper())
                                                  && u.UserName.ToUpper().Contains(searchAuthor.ToUpper())
                                                  orderby r.Date
                                  select r
              );

            //Category = new SelectList(await categoryQuery.ToListAsync());
            RecipeCategories = await recipeCategory.ToListAsync();
            
        }
    }
}

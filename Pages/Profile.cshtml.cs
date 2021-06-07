using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ProjektDotnet.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ILogger<ProfileModel> _logger;
        public string UserName { get; set; }
        public ProfileModel(ILogger<ProfileModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string username)
        {
            UserName = username;
        }
    }
}

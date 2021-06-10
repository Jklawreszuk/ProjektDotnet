using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjektDotnet.Models
{
    public class RecipeViewModel
    {
        [Required(ErrorMessage = "Tytuł jest wymagany")]  
        [Display(Name = "First Name")]  
        public string Name { get; set; }  
        public string Description { get; set; }  
  
        [Required(ErrorMessage = "Składniki są wymagane")]  
        public ICollection<SelectListItem> Ingredients { get; set; }  
  
        [Required(ErrorMessage = "Wybierz kategorie")]  
        public ICollection<Category> Categories { get; set; }   
        public IFormFile ProfileImages { get; set; }  
    }
}
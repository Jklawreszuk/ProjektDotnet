using System.ComponentModel.DataAnnotations;

namespace ProjektDotnet.Models;
public class RecipeViewModel
{
    [Required(ErrorMessage = "Id jest wymagane")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tytuł jest wymagany")]
    [Display(Name = "Tytuł")]
    public string Name { get; set; }
    [Display(Name = "Opis")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Składniki są wymagane")]
    public string[] Ingredients { get; set; }

    [Required(ErrorMessage = "Wybierz kategorie")]
    public string[] Categories { get; set; }
    public IEnumerable<IFormFile> ProfileImages { get; set; }
}

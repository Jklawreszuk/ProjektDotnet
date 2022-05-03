using System.ComponentModel.DataAnnotations;

namespace ProjektDotnet.Models;
public class Category
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<RecipeCategory> RecipeCategories { get; set; }
}
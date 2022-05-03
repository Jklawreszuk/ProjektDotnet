using System.ComponentModel.DataAnnotations;

namespace ProjektDotnet.Models;
public class Recipe
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [MaxLength(500)]
    public string Description { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; }
    public DateTime Date { get; set; }
    public ICollection<Images> Images { get; set; }
    public ICollection<RecipeCategory> RecipeCategories { get; set; }
    public ICollection<Favourites> Favourites { get; set; }
    public int LikeCount { get; set; }
    public int DislikeCount { get; set; }
    public ICollection<Ratings> Ratings { get; set; }
}
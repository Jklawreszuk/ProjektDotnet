using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektDotnet.Models;
public class RecipeCategory
{
    public int RecipeId { get; set; }

    [ForeignKey("RecipeId")]
    public Recipe Recipe { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
}

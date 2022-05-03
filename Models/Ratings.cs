namespace ProjektDotnet.Models;
public class Ratings
{
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public bool IsLike { get; set; }
}


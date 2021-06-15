using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjektDotnet.Models;

public static class Utilis
{
    public static IEnumerable<Ingredient> GetIngredients(string[] selectList)
    {
        foreach (var item in selectList)
        {
            yield return new Ingredient() { Name = item };
        }
    }
    public static IEnumerable<RecipeCategory> GetRecipeCategories(string[] selectList,List<Category> category)
    {
        foreach (var item in selectList)
        {
            yield return new RecipeCategory() 
            {
                CategoryId = category.Where(p=>p.Name==item).First().Id 
            };
        }
    }
    
    public static IEnumerable<Images> UploadedFile(RecipeViewModel model)
    {

        if (model.ProfileImages != null)
        {

            foreach (var img in model.ProfileImages)
            {
                using var memoryStream = new MemoryStream();

                img.CopyTo(memoryStream);

                // Upload the file if less than 2 MB
                if (memoryStream.Length < 2097152)
                {
                    var file = new Images() { Image = memoryStream.ToArray() };
                    yield return file;
                }
                else
                {
                    throw new Exception();
                }



            }

        }
    }
}
namespace allspice.Models;

public class FavoritedRecipe : RepoItem<int>
{
public int RecipeId { get; set; }
public string AccountId { get; set; }
}

public class UserFavoritedRecipe : Recipe
{
    public int RecipeId { get; set; }
    public string AccountId { get; set; }

    public int FavoriteId { get; set; }
}






namespace allspice.Services;

public class FavoritedRecipesService
{
    private readonly FavoritedRecipesRepository _favoritedRecipesRepository;

    public FavoritedRecipesService(FavoritedRecipesRepository favoritedRecipesRepository)
    {
        _favoritedRecipesRepository = favoritedRecipesRepository;
    }

    internal UserFavoritedRecipe CreateFavorite(UserFavoritedRecipe favoriteData)
    {
        UserFavoritedRecipe recipe = _favoritedRecipesRepository.CreateFavorite(favoriteData);
        return recipe;
    }

    internal List<UserFavoritedRecipe> GetAccountFavoritedRecipes(string userId)
    {
        List<UserFavoritedRecipe> recipes = _favoritedRecipesRepository.GetAccountFavoritedRecipes(userId);
        return recipes;
    }

    internal string UnfavoriteRecipe(int favoriteFavoriteId, string userId)
    {
        FavoritedRecipe recipe = GetFavoritedRecipeById(favoriteFavoriteId);
        if(recipe.AccountId != userId) throw new Exception("You cannot delete something you didn't make GOBLIN");
        _favoritedRecipesRepository.UnfavoriteRecipe(favoriteFavoriteId);
        return $"Recipe was unfavorited successfully";
    }

    private FavoritedRecipe GetFavoritedRecipeById(int favoritedRecipeId)
  {
    FavoritedRecipe favoritedRecipe = _favoritedRecipesRepository.GetFavoritedRecipeById(favoritedRecipeId);

    if (favoritedRecipe == null)
    {
      throw new Exception($"No favorited recipe found with the id of {favoritedRecipeId}");
    }

    return favoritedRecipe;
  }
}

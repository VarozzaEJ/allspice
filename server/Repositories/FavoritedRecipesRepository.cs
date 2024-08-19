







namespace allspice.Repositories;

public class FavoritedRecipesRepository
{
    private readonly IDbConnection _db;

    public FavoritedRecipesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal UserFavoritedRecipe CreateFavorite(UserFavoritedRecipe favoriteData)
    {
        string sql = @"
        INSERT INTO favoritedRecipes(accountId, recipeId)
        VALUES(@accountId, @recipeId);

        SELECT 
        favoritedRecipes.*,
        recipes.*,
        accounts.*
        
        FROM favoritedRecipes 
        JOIN recipes ON recipes.id = favoritedRecipes.recipeId
        JOIN accounts ON accounts.id = recipes.creatorId
        WHERE favoritedRecipes.id = LAST_INSERT_ID()
        ;";

        UserFavoritedRecipe recipe = _db.Query<FavoritedRecipe, UserFavoritedRecipe, Profile, UserFavoritedRecipe>(sql, (favorite, recipe, profile) => {
            recipe.AccountId = favorite.AccountId;
            recipe.RecipeId = favorite.RecipeId;
            recipe.FavoriteId = favorite.Id;
            recipe.Creator = profile;
            return recipe;
        }, favoriteData).FirstOrDefault();
        return recipe;
    }

    internal List<UserFavoritedRecipe> GetAccountFavoritedRecipes(string userId)
    {
        string sql = @"
        SELECT 
        favoritedRecipes.*,
        recipes.*,
        accounts.*

        FROM favoritedRecipes
        JOIN recipes ON recipes.id = favoritedRecipes.recipeId
        JOIN accounts on accounts.id = recipes.creatorId
        WHERE favoritedRecipes.accountId = @userId
        ;";

        List<UserFavoritedRecipe> recipes = _db.Query<FavoritedRecipe, UserFavoritedRecipe, Profile, UserFavoritedRecipe>(sql, (favorite, recipe, profile) => {
            recipe.Creator = profile;
            recipe.AccountId = favorite.AccountId;
            recipe.RecipeId = favorite.RecipeId;
            recipe.FavoriteId = favorite.Id;
            return recipe;
        }, new {userId}).ToList();
        return recipes;
    }

    internal FavoritedRecipe GetFavoritedRecipeById(int favoritedRecipeId)
    {
        string sql = @"
        SELECT * FROM favoritedRecipes WHERE favoritedRecipes.id = @favoritedRecipeId
        ;";


        FavoritedRecipe recipe = _db.Query<FavoritedRecipe>(sql, new{favoritedRecipeId}).FirstOrDefault();
        return recipe;
    }

    internal void UnfavoriteRecipe(int favoriteFavoriteId)
    {
        string sql = @"
        DELETE FROM favoritedRecipes WHERE id = @favoriteFavoriteId
        ;";

        _db.Execute(sql, new {favoriteFavoriteId});
    }
}

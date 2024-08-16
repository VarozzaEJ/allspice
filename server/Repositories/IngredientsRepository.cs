







namespace allspice.Repositories;

public class IngredientsRepository
{
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Ingredient CreateIngredient(Ingredient ingredientData)
    {
        string sql = @"
        INSERT INTO 
        ingredients(name, quantity, creatorId, recipeId)
        VALUES(@Name, @Quantity, @CreatorId, @RecipeId);

        SELECT
        *
        FROM ingredients
        WHERE ingredients.id = LAST_INSERT_ID();";

        Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
        return ingredient;
    }

    internal void DeleteIngredient(int ingredientId)
    {
        string sql = @"
            DELETE FROM ingredients WHERE id = @ingredientId LIMIT 1
        ;";

        int rowsAffected = _db.Execute(sql, new {ingredientId});

        if (rowsAffected == 0) throw new Exception("DELETE FAILED");
    if (rowsAffected > 1) throw new Exception("DELETE DID NOT FAIL, BUT THAT IS STILL A PROBLEM");
    }

    internal Ingredient GetIngredientById(int ingredientId)
    {
        string sql = @"
        SELECT
        ingredients.*,
        accounts.*
        FROM ingredients
        JOIN accounts ON accounts.id = ingredients.creatorId
        WHERE ingredients.id = @ingredientId
        ;";

        Ingredient ingredient = _db.Query<Ingredient, Profile, Ingredient>(sql, (ingredient, profile) => {
            ingredient.Creator = profile;
            return ingredient;
        }, new {ingredientId}).FirstOrDefault();
        return ingredient;
    }

    internal List<Ingredient> GetIngredientsForRecipe(int recipeId)
    {
        string sql = @"
        SELECT * FROM ingredients WHERE ingredients.recipeId = @recipeId
        ;";

        List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new {recipeId}).ToList();
        return ingredients;
    }
}

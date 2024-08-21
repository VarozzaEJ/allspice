



namespace allspice.Repositories;
public class RecipesRepository
{
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO 
        recipes(title, instructions, img, category, creatorId)
        VALUES(@Title, @Instructions, @Img, @Category, @CreatorId);

        Select
        recipes.*,
        accounts.*
        FROM recipes
        JOIN accounts ON accounts.id = recipes.creatorId
        WHERE recipes.id = LAST_INSERT_ID();";

        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile)=> {
            recipe.Creator = profile;
            return recipe;
        }, recipeData).FirstOrDefault();
        return recipe;
    }

    internal void DestroyRecipe(int recipeId)
    {
        string sql = @"
        DELETE FROM recipes WHERE id = @recipeId LIMIT 1
        ;";

        int rowsAffected = _db.Execute(sql, new { recipeId });

    if (rowsAffected == 0) throw new Exception("DELETE FAILED");
    if (rowsAffected > 1) throw new Exception("DELETE DID NOT FAIL, BUT THAT IS STILL A PROBLEM");
    
    }

    internal List<Recipe> GetAllRecipies()
    {
        string sql = @"
        SELECT 
        recipes.*,
        accounts.*
        FROM recipes
        JOIN accounts ON accounts.id = recipes.creatorId
        ;";

        List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) => {
            recipe.Creator = profile;
            return recipe;
        }).ToList();
        return recipes;
    }

    internal List<Recipe> GetMyMadeRecipes(string creatorId)
    {
        string sql = @"
        SELECT 
        recipes.*,
        accounts.*
        FROM recipes 
        JOIN accounts on accounts.id = recipes.creatorId
        WHERE recipes.creatorId = @creatorId
        ;";

        List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) => {
            recipe.Creator = profile;
            return recipe;
        }, new{creatorId}).ToList();
        return recipes;
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        string sql = @"
        SELECT 
        recipes.*,
        accounts.*
        FROM recipes
        JOIN accounts ON accounts.id = recipes.creatorId
        WHERE recipes.id = @recipeId
        ;";

        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, (recipe, profile) => {
            recipe.Creator = profile;
            return recipe;
        }, new { recipeId }).FirstOrDefault();
        return recipe;
    }

    internal void UpdateRecipe(Recipe recipe)
    {
        string sql = @"
        UPDATE recipes
        SET
        title = @Title,
        instructions = @Instructions,
        img = @Img,
        category = @Category
        WHERE
        id = @Id LIMIT 1
        ;";

        int rowsAffected = _db.Execute(sql, recipe);

        if(rowsAffected == 0) throw new Exception("Update did not work fella");
        
        if(rowsAffected > 1) throw new Exception("Update worked too well fella");
    }
}



namespace allspice.Services;

public class RecipesService
{
    private readonly RecipesRepository _recipesRepository;

    public RecipesService(RecipesRepository recipesRepository)
    {
        _recipesRepository = recipesRepository;
    }

    internal Recipe CreateRecipe(Recipe recipeData)
    {
       Recipe recipe =  _recipesRepository.CreateRecipe(recipeData);
       return recipe;
    }

    internal string DestroyRecipe(int recipeId, string userId)
    {
        Recipe recipe = GetRecipeById(recipeId);

        if(recipe.CreatorId != userId) throw new Exception("You cannot delete a recipe you didn't create GOBLIN GOBLIN");

        _recipesRepository.DestroyRecipe(recipeId);
        return $"{recipe.Title} was deleted";
    }

    internal List<Recipe> GetAllRecipes()
    {
        List<Recipe> recipes = _recipesRepository.GetAllRecipies();
        return recipes;
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        Recipe recipe = _recipesRepository.GetRecipeById(recipeId);

        if(recipe == null) throw new Exception($"No recipe with the id of {recipeId} was found");

        return recipe;
    }

    internal Recipe UpdateRecipe(string userId, int recipeId, Recipe recipeData)
    {
        Recipe recipe = GetRecipeById(recipeId);

        if (recipe.CreatorId != userId) throw new Exception("You cannot update a recipe you did not create");

        recipe.Category = recipeData.Category ?? recipe.Category;
        recipe.Img = recipeData.Img ?? recipe.Img;
        recipe.Instructions = recipeData.Instructions ?? recipe.Instructions;
        recipe.Title = recipeData.Title ?? recipe.Title;

        _recipesRepository.UpdateRecipe(recipe);

        return recipe;
    }
}
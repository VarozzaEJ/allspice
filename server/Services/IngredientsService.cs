





namespace allspice.Services;

public class IngredientsService
{
    private readonly IngredientsRepository _ingredientsRepository;

    public IngredientsService(IngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    internal Ingredient CreateIngredient(Ingredient ingredientData)
    {
        Ingredient ingredient = _ingredientsRepository.CreateIngredient(ingredientData);
        return ingredient;
    }

    internal string DeleteIngredient(int ingredientId, string userId)
    {
        Ingredient ingredient = GetIngredientById(ingredientId);

        if(ingredient.CreatorId != userId) throw new Exception("You cannot delete an ingredient you didn't create GOBLIN GOBLIN");
        _ingredientsRepository.DeleteIngredient(ingredientId);
        return $"{ingredient.Name} was deleted";
    }

    internal List<Ingredient> GetIngredientsForRecipe(int recipeId)
    {
        List<Ingredient> ingredients = _ingredientsRepository.GetIngredientsForRecipe(recipeId);
        return ingredients;
    }

    internal Ingredient GetIngredientById(int ingredientId)
    {
        Ingredient ingredient = _ingredientsRepository.GetIngredientById(ingredientId);

        if(ingredient == null) throw new Exception($"No ingredient with the id of {ingredientId} was found");
        return ingredient;
    }
}

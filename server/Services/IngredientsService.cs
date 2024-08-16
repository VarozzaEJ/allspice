



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
}

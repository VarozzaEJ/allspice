using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly RecipesService _recipesService;

    private readonly IngredientsService _ingredientsService;
        private readonly Auth0Provider _auth0Provider;

    public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider, IngredientsService ingredientsService)
    {
        _recipesService = recipesService;
        _auth0Provider = auth0Provider;
        _ingredientsService = ingredientsService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
    {
        try 
        {
        Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        recipeData.CreatorId = userInfo.Id;
        Recipe recipe = _recipesService.CreateRecipe(recipeData);
        return Ok(recipe);
        }
        catch (Exception exception)
        {
          return BadRequest(exception.Message);
        }
    }


[HttpGet]
    public ActionResult<List<Recipe>> GetAllRecipes() 
    {
        try 
        {
        List<Recipe> recipes = _recipesService.GetAllRecipes();
        return Ok(recipes);
        }
        catch (Exception exception)
        {
          return BadRequest(exception.Message);
        }
    }

    [HttpGet("{recipeId}")]
    public ActionResult<Recipe> GetRecipeById(int recipeId)
    {
        try 
        {
        Recipe recipe = _recipesService.GetRecipeById(recipeId);
        return Ok(recipe);
        }
        catch (Exception exception)
        {
          return BadRequest(exception.Message);
        }
    }

    [HttpPut("{recipeId}")]
    [Authorize]
    public async Task<ActionResult<Recipe>> UpdateRecipe(int recipeId, [FromBody] Recipe recipeData)
    {
        try 
        {
        Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

        Recipe recipe = _recipesService.UpdateRecipe(userInfo.Id, recipeId, recipeData);

        return Ok(recipe);
        }
        catch (Exception exception)
        {
          return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{recipeId}")]
    [Authorize]

    public async Task<ActionResult<String>> DestroyRecipe(int recipeId)
    {
        try 
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _recipesService.DestroyRecipe(recipeId, userInfo.Id);
            return Ok(message);
        }
        catch (Exception exception)
        {
          return BadRequest(exception.Message);
        }
    }

    [HttpGet("{recipeId}/ingredients")]
    public ActionResult<List<Ingredient>> GetIngredientsForRecipe(int recipeId) 
    {
      try 
      {
        List<Ingredient> ingredients = _ingredientsService.GetIngredientsForRecipe(recipeId);
        return Ok(ingredients);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

    
}
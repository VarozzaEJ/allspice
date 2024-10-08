namespace allspice.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;

  private readonly FavoritedRecipesService _favoritedRecipesService;

  private readonly RecipesService _recipesService;

    public AccountController(AccountService accountService, Auth0Provider auth0Provider, FavoritedRecipesService favoritedRecipesService, RecipesService recipesService)
    {
        _accountService = accountService;
        _auth0Provider = auth0Provider;
        _favoritedRecipesService = favoritedRecipesService;
        _recipesService = recipesService;
    }

    [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("favorites")]
  public async Task<ActionResult<List<UserFavoritedRecipe>>> GetAccountFavoritedRecipes()
  {
    try 
    {
    Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
    List<UserFavoritedRecipe> recipes = _favoritedRecipesService.GetAccountFavoritedRecipes(userInfo.Id);
    return Ok(recipes);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{creatorId}/recipes")]
    public async Task<ActionResult<List<Recipe>>> GetMyMadeRecipes(string creatorId) {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Recipe> recipes = _recipesService.GetMyMadeRecipes(creatorId);
      return Ok(recipes);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }
}

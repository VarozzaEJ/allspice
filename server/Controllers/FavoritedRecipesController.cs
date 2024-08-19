namespace allspice.Controllers;

[ApiController]
[Route("api/favorites")]

public class FavoritedRecipesController : ControllerBase
{
  private readonly FavoritedRecipesService _favoritedRecipesService;
  private readonly Auth0Provider _auth0Provider;

    public FavoritedRecipesController(FavoritedRecipesService favoritedRecipesService, Auth0Provider auth0Provider)
    {
        _favoritedRecipesService = favoritedRecipesService;
        _auth0Provider = auth0Provider;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<UserFavoritedRecipe>> CreateFavorite([FromBody] UserFavoritedRecipe favoriteData)
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      favoriteData.AccountId = userInfo.Id;
      UserFavoritedRecipe favorite = _favoritedRecipesService.CreateFavorite(favoriteData);
      return Ok(favorite);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }

    [HttpDelete("{favoriteFavoriteId}")]
    [Authorize]
    public async Task<ActionResult<string>> UnfavoriteRecipe(int favoriteFavoriteId)
    {
      try 
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _favoritedRecipesService.UnfavoriteRecipe(favoriteFavoriteId, userInfo.Id);
      return Ok(message);
      }
      catch (Exception exception)
      {
        return BadRequest(exception.Message);
      }
    }


    
}

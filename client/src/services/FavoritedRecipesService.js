import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Recipe } from "@/models/Recipe.js"
import { AppState } from "@/AppState.js"
import { FavoritedRecipe } from "@/models/FavoritedRecipe.js"

class FavoritedRecipesService {
    async deleteFavorite(favoriteId) {
        await api.delete(`api/favorites/${favoriteId}`)
        const foundRecipe = AppState.favoritedRecipes.findIndex(recipe => recipe.favoriteId == favoriteId)
        AppState.favoritedRecipes.splice(foundRecipe, 1)
        const newUnfavoritedRecipe = new Recipe(foundRecipe)
        AppState.recipes.push(newUnfavoritedRecipe)
    }
    async favoriteRecipe(recipeData) {
        const response = await api.post("api/favorites", recipeData)
        const newRecipe = new FavoritedRecipe(response.data)
        const foundRecipe = AppState.recipes.findIndex(recipe => recipe.id == recipeData.recipeId)
        AppState.recipes.splice(foundRecipe, 1)
        AppState.favoritedRecipes.push(newRecipe)
    }
    async getAllFavoritedRecipes() {
        const response = await api.get('account/favorites')
        const favoritedRecipes = response.data.map(recipePOJO => new FavoritedRecipe(recipePOJO))
        AppState.favoritedRecipes = favoritedRecipes
        AppState.favoritedRecipes.forEach(recipe => recipe.isFavorited = true)
    }
}

export const favoritedRecipesService = new FavoritedRecipesService()
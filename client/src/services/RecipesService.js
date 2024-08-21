import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Recipe } from "@/models/Recipe.js"
import { AppState } from "@/AppState.js"
import { Ingredient } from "@/models/Ingredient.js"
import { FavoritedRecipe } from "@/models/FavoritedRecipe.js"


class RecipesService {
    async getIngredientsForRecipe(recipeId) {
        AppState.activeRecipeIngredients = []
        const response = await api.get(`api/recipes/${recipeId}/ingredients`)
        const listOfIngredients = response.data.map(ingredientPOJO => new Ingredient(ingredientPOJO))
        AppState.activeRecipeIngredients = listOfIngredients
    }
    async editRecipe(recipeData, recipeId) {
        const response = await api.put(`api/recipes/${recipeId}`, recipeData)
        // recipeData.ingredients.recipeId = recipeId
        // const ingredientResponse = await api.post('api/ingredients', recipeData.ingredients)
        // logger.log(ingredientResponse)
        const recipeIndex = await this.getRecipeIndexById(recipeId)
        const updatedRecipe = new Recipe(response.data)
        AppState.recipes.splice(recipeIndex, 1, updatedRecipe)
        AppState.activeRecipe.editMode = false
        AppState.activeRecipe = updatedRecipe
    }
    activateEditMode(recipeId) {
        const foundRecipe = AppState.activeRecipe
        foundRecipe.editMode = !foundRecipe.editMode
    }

    async getRecipeIndexById(recipeId) {
        const foundRecipe = await AppState.recipes.findIndex(recipe => recipe.id == recipeId)
        return foundRecipe
    }
    async deleteRecipe(recipeId) {
        await api.delete(`api/recipes/${recipeId}`)
        const recipeToDestroy = AppState.recipes.findIndex(recipe => recipe.id == recipeId)
        AppState.recipes.splice(recipeToDestroy, 1)
        AppState.favoritedRecipes.splice(recipeToDestroy, 1)
        AppState.myRecipes.splice(recipeToDestroy, 1)
    }
    setActiveRecipe(recipeProp) {
        AppState.activeRecipeIngredients = null
        AppState.activeRecipe = null
        const activeRecipe = new Recipe(recipeProp)
        activeRecipe.editMode = false
        AppState.activeRecipe = activeRecipe
    }
    async createRecipe(recipeData) {
        const response = await api.post('api/recipes', recipeData)
        const newRecipe = new FavoritedRecipe(response.data)
        AppState.recipes.push(newRecipe)
        AppState.myRecipes.push(newRecipe)
        return newRecipe
    }
    async getAllRecipes() {
        const response = await api.get('api/recipes')
        const recipes = response.data.map((recipePOJO) => new Recipe(recipePOJO))
        AppState.recipes = recipes
    }

    async getMyRecipes(creatorId) {
        const response = await api.get(`/account/${creatorId}/recipes`)
        const myRecipes = response.data.map(recipePOJO => new Recipe(recipePOJO))
        AppState.myRecipes = myRecipes
    }

}

export const recipesService = new RecipesService()
import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Recipe } from "@/models/Recipe.js"
import { AppState } from "@/AppState.js"


class RecipesService {
    async editRecipe(recipeData, recipeId) {
        const response = await api.put(`api/recipes/${recipeId}`, recipeData)
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
        await api.get(`api/recipes/${recipeId}`)
        const recipeToDestroy = AppState.recipes.findIndex(recipe => recipe.id == recipeId)
        AppState.recipes.splice(recipeToDestroy, 1)
    }
    setActiveRecipe(recipeProp) {
        const activeRecipe = new Recipe(recipeProp)
        activeRecipe.editMode = false
        AppState.activeRecipe = activeRecipe
    }
    async createRecipe(recipeData) {
        const response = await api.post('api/recipes', recipeData)
        const newRecipe = new Recipe(response.data)
        AppState.recipes.push(newRecipe)
        return newRecipe
    }
    async getAllRecipes() {
        const response = await api.get('api/recipes')
        const recipes = response.data.map((recipePOJO) => new Recipe(recipePOJO))
        AppState.recipes = recipes
    }

}

export const recipesService = new RecipesService()
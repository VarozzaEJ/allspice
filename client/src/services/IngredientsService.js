import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Ingredient } from "@/models/Ingredient.js"
import { AppState } from "@/AppState.js"


class IngredientsService {
    async deleteIngredient(ingredientId) {
        const response = await api.delete(`api/ingredients/${ingredientId}`)
        logger.log(`Ingredient with the id of ${ingredientId} was deleted`)
        const foundIngredient = AppState.activeRecipeIngredients.findIndex(ingredient => ingredient.id == ingredientId)
        AppState.activeRecipeIngredients.splice(foundIngredient, 1)
    }
    async addIngredientToRecipe(ingredientData, recipeId) {
        ingredientData.recipeId = recipeId
        const response = await api.post('api/ingredients', ingredientData)
        const newIngredient = new Ingredient(response.data)
        AppState.activeRecipeIngredients.push(newIngredient)
    }

}

export const ingredientsService = new IngredientsService()
import { Recipe } from "./Recipe.js";

export class FavoritedRecipe extends Recipe {
    constructor(data) {
        super(data)
        this.id = data.id || data.favoriteId
        this.accountId = data.accountId
        this.recipeId = data.recipeId
    }
}
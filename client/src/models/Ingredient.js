

export class Ingredient {
    constructor(data) {
        this.id = data.id || data._id
        this.name = data.name
        this.quantity = data.quantity
        this.recipeId = data.recipeId

    }
}
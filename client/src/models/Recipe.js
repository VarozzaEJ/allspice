import { Account } from "./Account.js"

export class Recipe {
    constructor(data) {
        this.id = data.id
        this.title = data.title
        this.category = data.category
        this.instructions = data.instructions
        this.img = data.img
        this.createdAt = new Date(data.createdAt)
        this.updatedAt = new Date(data.updatedAt)
        this.creator = data.creator ? new Account(data.creator) : null
        this.creatorId = data.creatorId
        this.editMode = false
        this.isFavorited = data.isFavorited || false
    }

    get recipeBackgroundImage() {
        return `url(${this.img})`
    }
}
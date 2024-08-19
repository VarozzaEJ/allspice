import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"

class FavoritedRecipesService {
    async getAllFavoritedRecipes() {
        const response = await api.get('account/favorites')
        logger.log(response.data)
    }
}

export const favoritedRecipesService = new FavoritedRecipesService()
<script setup>
import { AppState } from '@/AppState.js';
import { Recipe } from '@/models/Recipe.js';
import { favoritedRecipesService } from '@/services/FavoritedRecipesService.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';


const props = defineProps({ recipeProp: { type: Recipe, required: true } })




const foundRecipe = computed(() => AppState.favoritedRecipes.find(recipe => recipe.id == props.recipeProp.id))

async function setActiveRecipe() {
    try {
        recipesService.setActiveRecipe(props.recipeProp)
        getIngredientsForRecipe()
    }
    catch (error) {
        Pop.error("Could not open this recipe");
    }
}

async function getIngredientsForRecipe() {
    try {
        await recipesService.getIngredientsForRecipe(props.recipeProp.id)
    }
    catch (error) {
        Pop.error("Could not get ingredients");
        logger.error(error)
    }
}

async function favoriteRecipe() {
    try {

        const recipeData = {
            recipeId: props.recipeProp.id,
            isFavorited: true
        }
        await favoritedRecipesService.favoriteRecipe(recipeData)
    }
    catch (error) {
        Pop.error("Could not favorite recipe");
        logger.error(error)
    }
}

async function deleteFavorite() {
    try {
        logger.log(foundRecipe)
        await favoritedRecipesService.deleteFavorite(foundRecipe.value.favoriteId)
    }
    catch (error) {
        Pop.error("Could not unfavorite recipe");
    }
}
</script>


<template>
    <div class="shadow shadow-dark d-flex justify-content-between flex-column rounded container-fluid recipe-img">
        <div class="row d-flex justify-content-between">
            <div class="col-md-3 col-4">
                <span class="badge text-capitalize text-bg-dark fs-6 mt-2">{{ recipeProp.category }} </span>

            </div>
            <div class="col-md-2 col-2">
                <span v-if="!foundRecipe" @click="favoriteRecipe()" role="button"
                    class=" fs-4 mdi mdi-heart-outline text-danger"></span>
                <span v-else-if="foundRecipe" @click="deleteFavorite()" role="button"
                    class=" fs-4 mdi mdi-heart text-danger"></span>
            </div>
        </div>
        <div @click="setActiveRecipe()" data-bs-toggle="modal" data-bs-target="#recipeModal"
            class="row d-flex justify-content-center selectable">
            <div class="col-11 px-0 rounded bg-smokey mb-3 d-flex">
                <p class="fs-5 text-capitalize ms-1 mb-0 text-light text-shadow">{{ recipeProp.title }}</p>
            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.recipe-img {
    background-image: v-bind('recipeProp.recipeBackgroundImage');
    aspect-ratio: 1/1;
    height: 350px;
    width: 350px;
    background-position: center;

}

.text-shadow {
    text-shadow: 1px 1px black;
}

.bg-smokey {
    background: rgba(218, 218, 218, 0.5);
}
</style>
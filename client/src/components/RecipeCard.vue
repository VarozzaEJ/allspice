<script setup>
import { Recipe } from '@/models/Recipe.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { onMounted } from 'vue';


const props = defineProps({ recipeProp: { type: Recipe, required: true } })


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
</script>


<template>
    <div @click="setActiveRecipe()"
        class="shadow shadow-dark d-flex justify-content-between flex-column rounded container-fluid recipe-img"
        data-bs-toggle="modal" data-bs-target="#recipeModal">
        <div class="row d-flex justify-content-between">
            <div class="col-md-3 col-4">
                <span class="badge text-bg-dark mt-2">{{ recipeProp.category }} </span>

            </div>
            <div class="col-md-2 col-2">
                <span role="button" class=" fs-4 mdi mdi-heart-outline text-danger"></span>
            </div>
        </div>
        <div class="row d-flex justify-content-center">
            <div class="col-11 px-0 rounded bg-smokey mb-3 d-flex">
                <p class="fs-5 ms-1 mb-0 text-light text-shadow">{{ recipeProp.title }}</p>
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
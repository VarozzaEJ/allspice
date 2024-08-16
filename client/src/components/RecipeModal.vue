<script setup>
import { AppState } from '@/AppState.js';
import { Recipe } from '@/models/Recipe.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { computed, ref } from 'vue';


const activeRecipe = computed(() => AppState.activeRecipe)

// defineProps({ recipeProp: { type: Recipe, required: true } })
const account = computed(() => AppState.account)


const editableRecipeData = ref({
    title: '',
    category: '',
    instructions: '',
    ingredients: '',
})

async function deleteRecipe(recipeId) {
    try {
        const choice = await Pop.confirm("are you sure?", 'delete post')
        if (choice == false) {
            return
        }
        await recipesService.deleteRecipe(recipeId)
        Modal.getOrCreateInstance('#recipeModal').hide()
        Pop.success("Post Deleted!")
    }
    catch (error) {
        logger.error(error)
    }
}

async function activateEditMode(recipeId) {
    recipesService.activateEditMode(recipeId)
}

async function editRecipe() {
    try {
        recipesService.editRecipe(editableRecipeData.value, activeRecipe.value.id)
        Pop.success("Edited Recipe")
    }
    catch (error) {
        Pop.error("Could not edit Recipe");
        logger.error(error)
    }
}
</script>


<template>
    <div v-if="activeRecipe" class="modal" id="recipeModal" tabindex="-1">
        <div class="modal-dialog modal-xl ">
            <div class="modal-content">
                <div v-if="activeRecipe.editMode == false" class="container-fluid h-100">
                    <div class="row h-100">
                        <div class="col-md-6 px-0 h-100 ">
                            <img class="img-fluid " :src="activeRecipe.img" :alt="`${activeRecipe.title}'s image'`"
                                :title="`${activeRecipe.title}'s image'`">
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-12">
                                    <div class="d-flex">

                                        <p class="fs-4 mb-1 text-success">{{ activeRecipe.title }}</p>
                                        <div v-if="account?.id == activeRecipe.creator.id"
                                            class="dropdown-center flex-grow-1 d-flex justify-content-end">
                                            <p class="mdi mdi-dots-horizontal fs-1" href="#" role="button"
                                                data-bs-toggle="dropdown" aria-expanded="false">

                                            </p>
                                            <div class="dropdown-menu ">
                                                <div class="container">
                                                    <div class="row">
                                                        <div class="col d-flex justify-content-center">
                                                            <button @click="deleteRecipe(activeRecipe.id)"
                                                                class="btn w-75 d-flex justify-content-center mb-2 btn-danger"><i
                                                                    class="mdi mdi-delete-forever">Delete</i></button>
                                                        </div>
                                                        <div class="col d-flex justify-content-center">
                                                            <button @click="activateEditMode(activeRecipe.id)"
                                                                class="btn w-75 text-center mb-1 btn-success"><i
                                                                    class="mdi mdi-file"></i>Edit</button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class=" d-flex flex-column justify-content-center">


                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <p class="fs-6 mb-1 text-dark-subtle fw-lighter">{{ activeRecipe.creator.name }}</p>
                                    <span class="badge text-bg-dark fw-light mt-2">{{ activeRecipe.category }} </span>
                                    <p class="mb-1 mt-4 fs-4 ">Ingredients</p>
                                </div>
                            </div>
                            <div class="col-12">
                                <p class="mb-1 mt-4 fs-4">Instructions</p>
                                <p>{{ activeRecipe.instructions }}</p>
                            </div>


                        </div>
                    </div>
                </div>
                <div v-if="activeRecipe.editMode == true" class="container-fluid h-100">
                    <div class="row h-100">
                        <div class="col-md-6 px-0 h-100 ">
                            <img class="img-fluid " :src="activeRecipe.img" :alt="`${activeRecipe.title}'s image'`"
                                :title="`${activeRecipe.title}'s image'`">
                        </div>
                        <div class="col-md-6 d-flex justify-content-between flex-column">
                            <form @submit.prevent="editRecipe()">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="d-flex">

                                            <p class="fs-4 mb-1 text-success"></p>
                                            <div class="">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <label for="title">Title</label>
                                                        <input v-model="editableRecipeData.title" type="text"
                                                            class="text-success form-control">
                                                    </div>

                                                </div>
                                            </div>
                                            <div v-if="account?.id == activeRecipe.creator.id"
                                                class="dropdown-center w-50 flex-grow-1 d-flex justify-content-end">
                                                <p class="mdi mdi-dots-horizontal fs-1" href="#" role="button"
                                                    data-bs-toggle="dropdown" aria-expanded="false">

                                                </p>
                                                <div class="dropdown-menu ">
                                                    <div class="container">
                                                        <div class="row">
                                                            <div class="col d-flex justify-content-center">
                                                                <button @click="deleteRecipe(activeRecipe.id)"
                                                                    class="btn w-75 d-flex justify-content-center mb-2 btn-danger"><i
                                                                        class="mdi mdi-delete-forever">Delete</i></button>
                                                            </div>
                                                            <div class="col d-flex justify-content-center">
                                                                <button @click="activateEditMode(activeRecipe.id)"
                                                                    class="btn w-75 text-center mb-1 btn-success"><i
                                                                        class="mdi mdi-file"></i>Edit</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class=" d-flex flex-column justify-content-center">


                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <p class="fs-6 mb-1 text-dark-subtle fw-lighter">{{ activeRecipe.creator.name }}
                                        </p>
                                        <!-- <span class="badge text-bg-dark fw-light mt-2">{{ activeRecipe.category }}
                                        </span> -->
                                        <div class="col-7">

                                            <div class="mb-3">
                                                <label for="category" class="form-label">Category</label>
                                                <select v-model="editableRecipeData.category" class="form-select"
                                                    aria-label="Default select example">
                                                    <option value="breakfast">Breakfast</option>
                                                    <option value="lunch">Lunch</option>
                                                    <option value="dinner">Dinner</option>
                                                    <option value="snack">Snack</option>
                                                    <option value="dessert">Dessert</option>
                                                </select>
                                            </div>
                                        </div>
                                        <p class="mb-1 mt-4 fs-4 ">Ingredients</p>
                                        <div class="col-12 d-flex ">

                                            <textarea class="w-100" v-model="editableRecipeData.ingredients"
                                                name="ingredients" id="ingredients"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="row d-flex justify-content-between">

                                    <div class="col-12">
                                        <p class="mb-1 mt-4 fs-4">Instructions</p>
                                        <!-- <p>{{ activeRecipe.instructions }}</p> -->
                                        <div class="col-12 d-flex ">

                                            <textarea rows="5" class="w-100" v-model="editableRecipeData.instructions"
                                                name="ingredients" id="ingredients"></textarea>
                                        </div>
                                    </div>
                                    <div class="col-12 my-3 d-flex justify-content-end">
                                        <button class="btn btn-success">Save</button>
                                    </div>

                                </div>
                            </form>

                        </div>
                    </div>
                </div>



            </div>
        </div>
    </div>

</template>


<style lang="scss" scoped>
img {
    border-top-left-radius: 1%;
    border-bottom-left-radius: 1%;
    border-top-right-radius: 1%;
    border-bottom-right-radius: 1%;
}
</style>
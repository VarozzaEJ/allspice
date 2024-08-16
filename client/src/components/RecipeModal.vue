<script setup>
import { AppState } from '@/AppState.js';
import { Recipe } from '@/models/Recipe.js';
import { ingredientsService } from '@/services/IngredientsService.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { computed, onMounted, ref } from 'vue';


const activeRecipe = computed(() => AppState.activeRecipe)
const activeRecipesIngredients = computed(() => AppState.activeRecipeIngredients)


// defineProps({ recipeProp: { type: Recipe, required: true } })
const account = computed(() => AppState.account)


onMounted(() => {
})



const editableRecipeData = ref({
    title: '',
    category: '',
    instructions: '',

})

const editableIngredientData = ref({
    quantity: '',
    name: '',
    recipeId: null
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
        logger.log(editableRecipeData.value)
        recipesService.editRecipe(editableRecipeData.value, activeRecipe.value.id)
        Pop.success("Edited Recipe")
    }
    catch (error) {
        Pop.error("Could not edit Recipe");
        logger.error(error)
    }
}

function addIngredientToRecipe() {
    try {
        ingredientsService.addIngredientToRecipe(editableIngredientData.value, activeRecipe.value.id)
        resetIngredientForm()
    }
    catch (error) {
        Pop.error("Could not add ingredient", 'Error');
    }
}

function resetIngredientForm() {
    editableIngredientData.value = {
        name: '',
        quantity: '',
        recipeId: null
    }
}

async function deleteIngredient(ingredientId) {
    try {
        await ingredientsService.deleteIngredient(ingredientId)
    }
    catch (error) {
        Pop.error("Could not delete Ingredient");
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
                                    <div v-for="ingredient in activeRecipesIngredients"
                                        class="d-flex mb-2 border border-dark w-50">
                                        <p class="fs-6 mb-0 ms-1">{{ ingredient.quantity }}</p>
                                        <p class="fs-6 ms-2 mb-0">{{ ingredient.name }}</p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <p class="mb-1 mt-4 fs-4">Instructions</p>
                                <p>{{ activeRecipe.instructions }}</p>
                            </div>


                        </div>
                    </div>
                </div>

                <!-- ANCHOR Forms -->

                <div v-if="activeRecipe.editMode == true" class="container-fluid h-100">
                    <div class="row h-100">
                        <div class="col-md-6 px-0 h-100 ">
                            <img class="img-fluid " :src="activeRecipe.img" :alt="`${activeRecipe.title}'s image'`"
                                :title="`${activeRecipe.title}'s image'`">
                        </div>
                        <div class="col-md-6 d-flex justify-content-between flex-column">
                            <div class="row">
                                <div class="col-12">
                                    <div class="d-flex">

                                        <p class="fs-4 mb-1 text-success"></p>
                                        <div class="">
                                            <div class="row">
                                                <div class="col-12">
                                                    <!-- <label for="title">Title</label>
                                                        <input v-model="editableRecipeData.title" type="text"
                                                            class="text-success form-control"> -->
                                                    <p class="fs-4 text-success"> {{ activeRecipe.title }}</p>
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
                                                    <div class="row ">
                                                        <div class="col-12 d-flex justify-content-center">
                                                            <button @click="deleteRecipe(activeRecipe.id)"
                                                                class="btn w-75 d-flex justify-content-center mb-2 btn-danger"><i
                                                                    class="mdi mdi-delete-forever">Delete</i></button>
                                                        </div>
                                                        <div class="col-12 d-flex justify-content-center">
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
                                    <span class="badge text-bg-dark fw-light mt-2">{{ activeRecipe.category }}
                                    </span>
                                    <div class="col-7">

                                        <div class="mb-3">
                                            <!-- <label for="category" class="form-label">Category</label>
                                                <select v-model="editableRecipeData.category" class="form-select"
                                                    aria-label="Default select example">
                                                    <option value="breakfast">Breakfast</option>
                                                    <option value="lunch">Lunch</option>
                                                    <option value="dinner">Dinner</option>
                                                    <option value="snack">Snack</option>
                                                    <option value="dessert">Dessert</option>
                                                </select> -->

                                        </div>
                                    </div>
                                    <div class="d-flex flex-column">
                                        <p class="mb-1 mt-4 fs-4 ">Ingredients</p>
                                        <div v-for="ingredient in activeRecipesIngredients"
                                            class="d-flex border align-items-center border-dark w-50 h-50 mb-2">
                                            <p role="button" @click="deleteIngredient(ingredient.id)"
                                                class=" text-danger mb-0 fs-5 ms-2 me-3">X</p>
                                            <p class="fs-6 mb-0">{{ ingredient.quantity }}</p>
                                            <p class="fs-6 mb-0 ms-2">{{ ingredient.name }}</p>
                                        </div>
                                        <form @submit.prevent="addIngredientToRecipe()">
                                            <div class="row">
                                                <div class="col-4 d-flex pe-1">
                                                    <div class="input-group mb-3">
                                                        <input v-model="editableIngredientData.quantity" type="text"
                                                            class="form-control" placeholder="Quantity"
                                                            aria-label="Ingredient Quantity"
                                                            aria-describedby="button-addon2">
                                                    </div>
                                                </div>
                                                <div class="col-5 ps-1">
                                                    <div class="input-group mb-3 ">
                                                        <input v-model="editableIngredientData.name" type="text"
                                                            class="form-control" placeholder="Name"
                                                            aria-label="Ingredient Name"
                                                            aria-describedby="button-addon2">

                                                    </div>
                                                </div>

                                                <div class="col-1 d-flex">
                                                    <button class="d-flex align-items-center btn btn-secondary"><i
                                                            class="mdi mdi-plus"></i></button>
                                                </div>
                                            </div>
                                        </form>
                                    </div>

                                </div>
                            </div>




                            <form @submit.prevent="editRecipe()">

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
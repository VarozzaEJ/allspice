<script setup>
import { Account } from '@/models/Account.js';
import { recipesService } from '@/services/RecipesService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { ref } from 'vue';


defineProps({
    account: Account
})

const editableRecipeData = ref({
    title: '',
    // file: null,
    // previewUrl: '',
    category: '',
    img: ''
})

async function createRecipe() {
    try {
        const newRecipe = await recipesService.createRecipe(editableRecipeData.value)
        Modal.getOrCreateInstance('#newRecipeForm').hide()
        Pop.success("Created Post")
        resetForm()
        recipesService.setActiveRecipe(newRecipe)
        Modal.getOrCreateInstance('#recipeModal').show()
        await recipesService.getAllRecipes()
    }
    catch (error) {
        Pop.error("Error creating post");
        logger.error(error)
    }
}

function resetForm() {
    editableRecipeData.value = {
        title: '',
        category: '',
        img: ''
    }
}
</script>


<template>
    <form v-if="account" @submit.prevent="createRecipe()">

        <div class="row d-flex justify-content-center">
            <div class="col-md-6">

                <div class="mb-3">
                    <label for="title" class="form-label">Title</label>
                    <input required v-model="editableRecipeData.title" type="text" class="form-control" id="title"
                        placeholder="" maxlength="275">
                </div>
            </div>
            <div class="col-md-6">

                <div class="mb-3">
                    <label for="category" class="form-label">Category</label>
                    <select required v-model="editableRecipeData.category" class="form-select"
                        aria-label="Default select example">
                        <option value="breakfast">Breakfast</option>
                        <option value="lunch">Lunch</option>
                        <option value="dinner">Dinner</option>
                        <option value="snack">Snack</option>
                        <option value="dessert">Dessert</option>
                    </select>
                </div>
            </div>
            <div class="col-8 ">
                <label class="form-label">Image Preview</label>
                <div class="d-flex justify-content-center">
                    <div v-if="!editableRecipeData.img"
                        class="bg-subtle rounded img-preview d-flex justify-content-center align-items-center">
                        <i class="mdi mdi-image display-1"></i>
                    </div>
                    <div v-else>
                        <img class="rounded img-preview preview-img" :src="editableRecipeData.img"
                            :alt="`${editableRecipeData.img}`">
                    </div>
                </div>
            </div>
            <div class="col-12">
                <div class="mb-3">
                    <label for="imgUrl" class="form-label">Image Url</label>
                    <input required v-model="editableRecipeData.img" name="img" type="url" class="form-control" id="img"
                        maxlength="1000">
                </div>
            </div>
        </div>
        <div class="row d-flex justify-content-end">
            <div class="col-md-2 col-4 d-flex justify-content-center">
                <button type="button" class="btn btn-outline-light text-dark" data-bs-dismiss="modal"
                    aria-label="Close">Cancel</button>
            </div>
            <div class="col-md-2 col-4 d-flex justify-content-center">
                <button class="btn btn-success">Submit</button>
            </div>
        </div>
    </form>
</template>


<style lang="scss" scoped>
.preview-img {
    width: 300px;
    height: 300px;
}

.img-preview {
    height: 25vh;
    width: 25vh;
    border: 2px dashed black;
    color: black;
}
</style>
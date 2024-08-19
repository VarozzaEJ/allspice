<script setup>
import { AppState } from '@/AppState.js';
import { favoritedRecipesService } from '@/services/FavoritedRecipesService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';


const favoritedRecipes = computed(() => AppState.favoritedRecipes)


onMounted(() => {
})


</script>


<template>
    <header>
        <div class="container-fluid bg-hero">
            <div class="row">
                <Login />
            </div>
            <div class="row d-flex justify-content-center">
                <div class="col-md-6 text-light text-shadow d-flex flex-column justify-content-center bg-transparent">
                    <p class="fs-1 fw-bold text-center mb-2"> AllSpice</p>
                    <p class="fs-2 fw-bold text-center mb-2"> Cherish Your Family</p>
                    <p class="fs-2 fw-bold text-center mb-2"> And Their Cooking</p>

                </div>

            </div>
            <div class="row d-flex justify-content-center">
                <div class="col-md-6 d-flex justify-content-center">
                    <RouterLink :to="{ name: 'Home' }">
                        <button class="btn no-round me-1 shadow btn-light">Home</button>
                    </RouterLink>
                    <button class="btn no-round me-1 shadow btn-light">My Recipes</button>
                    <RouterLink :to="{ name: 'Favorites' }">
                        <button class="btn no-round me-1 shadow btn-light">Favorites</button>
                    </RouterLink>
                </div>
            </div>
        </div>
    </header>
    <div class="container">
        <div class="row">
            <div class="col-12">

            </div>
        </div>
    </div>
</template>


<style lang="scss" scoped>
.fab {
    border-radius: 75%;
    position: fixed;
    bottom: 1em;
    right: 1em;
    padding: .5em;
    border: none;
    box-shadow: var(--shadow);
    z-index: 999;
    outline: none;
    width: 75px;
    height: 75px;
    text-shadow: 1px 1px black;

}

.no-round {
    border-radius: 3%;
}

.bg-hero {
    background-image: url("https://images.unsplash.com/photo-1696762314476-07470ad680bc?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");
    background-position: center;
    object-fit: cover;
    height: 250px;
}

.text-shadow {
    text-shadow: 2px 2px black;
}
</style>
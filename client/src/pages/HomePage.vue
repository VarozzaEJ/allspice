<script setup>
import { AppState } from '@/AppState.js';
import Login from '@/components/Login.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import RecipeModal from '@/components/RecipeModal.vue';
import { recipesService } from '@/services/RecipesService.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { RouterLink } from 'vue-router';


const account = computed(() => AppState.account)
const recipes = computed(() => AppState.recipes)

onMounted(() => {
  getAllRecipes()
})

async function getAllRecipes() {
  try {
    await recipesService.getAllRecipes()
  }
  catch (error) {
    Pop.error("Error getting recipes");
  }
}

</script>

<template>
  <header>
    <div class="container-fluid bg-hero">
      <div class="row">
        <Login />
      </div>
      <div class="row d-flex justify-content-center">
        <div
          class="col-md-6 sahitya-font text-light text-shadow d-flex flex-column justify-content-center bg-transparent">
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

  <main class="mt-5">
    <div class="container-fluid">
      <div class="row">

        <div v-for="recipe in recipes" :key="recipe.id" class="col-xl-4 col-md-6 mt-3">
          <RecipeCard :recipeProp='recipe' />
        </div>
      </div>
    </div>
  </main>

  <footer class="">
    <button data-bs-toggle="modal" data-bs-target="#newRecipeForm"
      class="fab btn shadow btn-success justify-content-end" title="New Recipe">
      <i class="mdi text-light mdi-plus fs-1"></i>
    </button>
  </footer>

  <RecipeModal modalId="recipeModal" modalName="recipeModal" />
</template>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

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

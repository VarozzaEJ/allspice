<script setup>
import { AppState } from '@/AppState.js';
import Login from '@/components/Login.vue';
import RecipeCard from '@/components/RecipeCard.vue';
import RecipeModal from '@/components/RecipeModal.vue';
import { AuthService } from '@/services/AuthService.js';
import { recipesService } from '@/services/RecipesService.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted, ref } from 'vue';
import { RouterLink } from 'vue-router';

const identity = computed(() => AppState.identity)
const account = computed(() => AppState.account)
async function login() {
  AuthService.loginWithPopup()
}
async function logout() {
  AuthService.logout()
}
const categoryFilter = ref('all')

const searchData = ref({
  searchData: ''
})


const recipes = computed(() => {
  if (categoryFilter.value == 'all' || categoryFilter.value == '') {
    return AppState.recipes
  }
  return AppState.recipes.filter(recipe => recipe.category == categoryFilter.value)
})

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

async function searchCategory() {
  categoryFilter.value = searchData.value.searchData
}

</script>

<template>
  <header>
    <div class="container-fluid bg-hero shadow-lg">
      <div class="row">
        <nav class="navbar bg-transparent">
          <div class="container-fluid d-flex justify-content-end">
            <form @submit="searchCategory()" class="d-flex me-md-5 me-1" role="search">
              <input v-model="searchData.searchData" class="form-control me-0 no-round-end" type="search"
                placeholder="Search By Category" aria-label="Search">
              <button class="btn btn-success no-round-start" type="submit">Search</button>
            </form>
            <button class="btn selectable text-success lighten-30 text-uppercase my-2 my-lg-0" @click="login"
              v-if="!identity">
              Login
            </button>
            <div v-else>
              <div class=" my-2 my-lg-0">
                <div data-bs-toggle="dropdown" aria-expanded="false" class="dropdown bg-transparent border-0 no-select">
                  <div class=" d-flex justify-content-end" v-if="account?.picture || identity?.picture">
                    <img type="button" :src="account?.picture || identity?.picture" alt="account photo" height="40"
                      class="rounded" />
                  </div>
                </div>
                <div class="dropdown-menu dropdown-menu-end dropdown-menu-start p-0" aria-labelledby="authDropdown">
                  <div class="list-group">
                    <router-link :to="{ name: 'Account' }">
                      <div class="list-group-item dropdown-item list-group-item-action">
                        Manage Account
                      </div>
                    </router-link>
                    <div class="list-group-item dropdown-item list-group-item-action text-danger selectable"
                      @click="logout">
                      <i class="mdi mdi-logout"></i>
                      logout
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </nav>
      </div>
      <div class="row d-flex justify-content-center">
        <div
          class="col-md-6 sahitya-font text-light text-shadow d-flex flex-column justify-content-center bg-transparent">
          <p class="fs-1 fw-bold text-center mb-2"> AllSpice</p>
          <p class="fs-3 fw-bold text-center mb-2"> Cherish Your Family</p>
          <p class="fs-3 fw-bold text-center mb-2"> And Their Cooking</p>

        </div>

      </div>
      <div class="row sahitya-font d-flex justify-content-center">
        <div class="col-md-6 d-flex justify-content-center">
          <RouterLink :to="{ name: 'Home' }">
            <button class="btn no-round text-success me-1 shadow btn-light">Home</button>
          </RouterLink>
          <RouterLink :to="{ name: 'MyRecipes' }">
            <button class="btn no-round text-success me-1 shadow btn-light">My Recipes</button>
          </RouterLink>
          <RouterLink :to="{ name: 'Favorites' }">
            <button class="btn no-round text-success me-1 shadow btn-light">Favorites</button>
          </RouterLink>
        </div>
      </div>
    </div>
  </header>

  <main class="mt-5">
    <div class="container-fluid">
      <div class="row">

        <div v-for="recipe in recipes" :key="recipe.id" class="col-xl-4 col-md-6 mt-3 mb-3">
          <RecipeCard :recipeProp='recipe' />
        </div>
      </div>
    </div>
  </main>

  <footer class="">
    <button v-if="account" data-bs-toggle="modal" data-bs-target="#newRecipeForm"
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

.no-round-start {
  border-top-left-radius: 0%;
  border-bottom-left-radius: 0%;
}

.no-round-end {
  border-top-right-radius: 0%;
  border-bottom-right-radius: 0%;
}

.bg-hero {
  background-image: url("https://images.unsplash.com/photo-1696762314476-07470ad680bc?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");
  background-position: center;
  object-fit: cover;
  height: 250px;
}

.text-shadow {
  text-shadow: 1px 1px black;
}
</style>

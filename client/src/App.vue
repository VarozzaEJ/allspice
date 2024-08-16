<script setup>
import { computed } from 'vue';
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import RecipeForm from './components/RecipeForm.vue';

const account = computed(() => AppState.account)
</script>

<template>
  <header>
    <!-- <Navbar /> -->
  </header>
  <main>
    <router-view />
  </main>
  <ModalWrapper modalId="newRecipeForm" modalName="Create Recipe" modalSize="lg">
    <RecipeForm :account="account" />
  </ModalWrapper>
</template>

<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>

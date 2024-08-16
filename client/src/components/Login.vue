<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState';
import { AuthService } from '../services/AuthService';

const identity = computed(() => AppState.identity)
const account = computed(() => AppState.account)
async function login() {
  AuthService.loginWithPopup()
}
async function logout() {
  AuthService.logout()
}

</script>

<template>
  <nav class="navbar bg-transparent">
    <div class="container-fluid d-flex justify-content-end">
      <form class="d-flex me-5" role="search">
        <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
        <button class="btn btn-success" type="submit">Search</button>
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
              <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
                <i class="mdi mdi-logout"></i>
                logout
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </nav>
  <!-- <span class="navbar-text">
    <button class="btn selectable text-success lighten-30 text-uppercase my-2 my-lg-0" @click="login" v-if="!identity">
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
            <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span> -->
</template>

<style lang="scss" scoped></style>

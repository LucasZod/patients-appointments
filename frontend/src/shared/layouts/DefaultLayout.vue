<template>
  <div class="flex h-screen flex-col">
    <AppNavbar />
    <div class="flex flex-1 overflow-hidden">
      <AppSidebar />
      <main class="flex-1 overflow-auto bg-bg p-6">
        <RouterView />
      </main>
    </div>
  </div>
</template>

<script setup lang="ts">
import { RouterView } from 'vue-router'
import AppNavbar from './partials/AppNavbar.vue'
import AppSidebar from './partials/AppSidebar.vue'
</script>

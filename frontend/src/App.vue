<template>
  <Toaster position="top-right" :duration="4000" />
  <RouterView />
</template>

<script setup lang="ts">
import { RouterView } from 'vue-router'
import { Toaster } from 'vue-sonner'
import 'vue-sonner/style.css'
</script>

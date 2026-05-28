<template>
  <div class="flex items-center gap-3">
    <AppButton variant="ghost" @click="router.push(`/orders/${route.params.id}`)">Voltar</AppButton>
    <h1 class="text-lg font-semibold text-text">Conferência de Amostras</h1>
  </div>
</template>

<script setup lang="ts">
import { useRouter, useRoute } from 'vue-router'
import AppButton from '@/shared/ui/AppButton.vue'

const router = useRouter()
const route = useRoute()
</script>

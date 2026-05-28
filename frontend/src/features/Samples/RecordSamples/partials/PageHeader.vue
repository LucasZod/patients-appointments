<template>
  <div class="flex items-center gap-3">
    <AppButton class="bg-none" variant="ghost" @click="router.back()">Voltar</AppButton>
    <div>
      <h1 class="text-lg font-semibold text-text">Registrar Amostras</h1>
      <p class="text-sm text-secondary">{{ currentOrder?.patientName }}</p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppButton from '@/shared/ui/AppButton.vue'

const router = useRouter()
const { currentOrder } = storeToRefs(useServiceOrderStore())
</script>

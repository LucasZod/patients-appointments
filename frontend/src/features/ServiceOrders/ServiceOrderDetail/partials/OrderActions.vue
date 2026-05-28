<template>
  <AppCard v-if="currentOrder!.status === 'InProgress'">
    <h2 class="text-sm font-semibold text-text mb-3">Ações</h2>
    <AppButton variant="primary" class="w-full" @click="completeCollection(currentOrder!.id)">
      Finalizar Coleta
    </AppButton>
  </AppCard>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppCard from '@/shared/ui/AppCard.vue'
import AppButton from '@/shared/ui/AppButton.vue'

const store = useServiceOrderStore()
const { currentOrder } = storeToRefs(store)
const { completeCollection } = store
</script>

<template>
  <AppCard>
    <div class="flex flex-col gap-2">
      <h2 class="text-sm font-semibold text-secondary uppercase">Prioridade</h2>
      <AppSegmented :options="options" :model-value="selectedPriority" @update:model-value="setPriority" />
    </div>
  </AppCard>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppCard from '@/shared/ui/AppCard.vue'
import AppSegmented from '@/shared/ui/AppSegmented.vue'
import type { Priority } from '@/types'

const store = useServiceOrderStore()
const { selectedPriority } = storeToRefs(store)
const { setPriority } = store

const options: Array<{ value: Priority; label: string }> = [
  { value: 'Normal', label: 'Normal' },
  { value: 'Preferred', label: 'Preferencial' },
  { value: 'Urgent', label: 'Urgente' },
]
</script>

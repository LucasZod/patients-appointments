<template>
  <AppCard>
    <div class="flex flex-col gap-2">
      <h2 class="text-sm font-semibold text-secondary uppercase">Prioridade</h2>
      <div class="flex gap-2 justify-evenly md:justify-stretch">
        <button
          v-for="opt in options"
          :key="opt.value"
          type="button"
          :class="[
            'rounded-badge border px-4 py-1.5 text-sm font-medium transition-colors cursor-pointer',
            selectedPriority === opt.value
              ? 'border-primary bg-primary text-white'
              : 'border-border bg-surface text-text hover:bg-bg',
          ]"
          @click="setPriority(opt.value)"
        >
          {{ opt.label }}
        </button>
      </div>
    </div>
  </AppCard>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppCard from '@/shared/ui/AppCard.vue'
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

<template>
  <div class="flex items-center justify-between gap-4 py-3 border-b border-border last:border-0">
    <div class="flex items-center gap-3">
      <TubeDot :tube-type="tube.type as TubeType" />
      <div>
        <p class="text-sm font-medium text-text">{{ tubeLabel }}</p>
        <p class="text-xs text-secondary">Para: {{ tube.exams.join(', ') }}</p>
      </div>
    </div>
    <div class="flex items-center gap-2">
      <AppButton variant="outlined" class="w-8 h-8 p-0 text-center" @click="decrement">−</AppButton>
      <span class="w-6 text-center text-sm font-semibold text-text">{{ quantity }}</span>
      <AppButton variant="outlined" class="w-8 h-8 p-0 text-center" @click="increment">+</AppButton>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { storeToRefs } from 'pinia'
import { useSampleStore } from '@/stores/sample.store'
import TubeDot from '@/shared/ui/TubeDot.vue'
import AppButton from '@/shared/ui/AppButton.vue'
import type { TubeType } from '@/types'

const props = defineProps<{ tube: { type: string; exams: string[] } }>()

const tubeLabels: Record<string, string> = {
  purple: 'Tubo roxo (EDTA)',
  yellow: 'Tubo amarelo (Soro)',
  green: 'Tubo verde (Heparina)',
  urine: 'Coletor de urina',
}
const tubeLabel = computed(() => tubeLabels[props.tube.type] ?? props.tube.type)

const store = useSampleStore()
const { tubeQuantities } = storeToRefs(store)
const { setTubeQuantity } = store

const quantity = computed(() => tubeQuantities.value[props.tube.type] ?? 1)
const increment = () => setTubeQuantity(props.tube.type, quantity.value + 1)
const decrement = () => setTubeQuantity(props.tube.type, quantity.value - 1)
</script>

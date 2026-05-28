<template>
  <AppCard v-if="tubeSummary.length > 0">
    <div class="flex items-center gap-3">
      <h2 class="text-sm font-semibold text-secondary uppercase">Tubos necessários</h2>
      <ul class="flex flex-wrap items-center gap-3">
        <li v-for="item in tubeSummary" :key="item.tubeType" class="flex items-center gap-2">
          <TubeDot :tube-type="item.tubeType" />
          <span class="text-sm text-text"
            >{{ item.count }} {{ tubeLabel(item.tubeType, item.count) }}</span
          >
        </li>
      </ul>
    </div>
  </AppCard>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import TubeDot from '@/shared/ui/TubeDot.vue'
import AppCard from '@/shared/ui/AppCard.vue'
import type { TubeType } from '@/types'

const { tubeSummary } = storeToRefs(useServiceOrderStore())

const LABELS: Record<TubeType, [string, string]> = {
  purple: ['roxo', 'roxos'],
  yellow: ['amarelo', 'amarelos'],
  green: ['verde', 'verdes'],
  urine: ['frasco de urina', 'frascos de urina'],
}

const tubeLabel = (tubeType: TubeType, count: number) => {
  const [singular, plural] = LABELS[tubeType]
  return count === 1 ? singular : plural
}
</script>

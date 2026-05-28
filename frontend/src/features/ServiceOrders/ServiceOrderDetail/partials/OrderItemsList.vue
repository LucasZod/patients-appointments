<template>
  <AppCard>
    <h2 class="text-sm font-semibold text-text mb-3">Exames</h2>
    <ul class="flex flex-col divide-y divide-border">
      <li
        v-for="item in currentOrder!.items"
        :key="item.id"
        class="flex items-center justify-between gap-3 py-3 first:pt-0 last:pb-0"
      >
        <div class="flex items-center gap-2">
          <TubeDot :tube-type="item.tubeType as TubeType" />
          <div>
            <p class="text-sm font-medium text-text">{{ item.examName }}</p>
            <p class="text-xs text-secondary">{{ item.examCode }}</p>
          </div>
        </div>
        <StatusChip :status="item.status" />
      </li>
    </ul>
  </AppCard>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppCard from '@/shared/ui/AppCard.vue'
import TubeDot from '@/shared/ui/TubeDot.vue'
import StatusChip from '@/shared/ui/StatusChip.vue'
import type { TubeType } from '@/types'

const { currentOrder } = storeToRefs(useServiceOrderStore())
</script>

<template>
  <AppCard>
    <h2 class="mb-3 text-sm font-semibold text-text">Exames Solicitados</h2>
    <ul class="flex flex-col gap-2">
      <li
        v-for="item in currentOrder?.items ?? []"
        :key="item.id"
        class="flex items-center justify-between gap-3"
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

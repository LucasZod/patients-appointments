<template>
  <AppCard
    :class="[
      'flex flex-col gap-3',
      row.order.status === 'InProgress' ? 'border-l-4 border-warning' : '',
    ]"
    padding="sm"
  >
    <div class="flex items-start justify-between gap-2">
      <div>
        <div class="flex items-center gap-2">
          <span class="text-xs font-bold text-secondary">{{ positionLabel }}</span>
          <span class="text-sm font-semibold text-text">{{ row.order.patientName ?? '—' }}</span>
        </div>
        <span class="text-xs text-secondary">OS {{ shortId }}</span>
      </div>
      <div class="flex items-center gap-1.5 flex-shrink-0">
        <PriorityBadge :priority="row.order.priority" />
        <StatusChip :status="row.order.status" />
      </div>
    </div>

    <div class="flex items-center justify-between gap-2">
      <div class="flex items-center gap-3">
        <span class="text-xs text-secondary">{{ row.waitingMinutes }} min</span>
        <div class="flex items-center gap-1">
          <TubeDot v-for="tube in row.tubeTypes" :key="tube" :tube-type="tube" />
        </div>
      </div>

      <AppButton v-if="canCall" variant="primary" :loading="isCallingNext" @click="callNext">
        Chamar
      </AppButton>
      <AppButton v-else-if="row.order.status === 'InProgress'" variant="outlined" @click="goDetail">
        Ver Detalhes
      </AppButton>
      <AppButton v-else-if="row.order.status === 'Collected'" variant="outlined" @click="goReview">
        Conferir
      </AppButton>
      <span v-else-if="row.order.status === 'Waiting'" class="text-xs text-secondary">Na fila</span>
    </div>
  </AppCard>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { storeToRefs } from 'pinia'
import { useQueueStore, type QueueRow } from '@/stores/queue.store'
import AppCard from '@/shared/ui/AppCard.vue'
import AppButton from '@/shared/ui/AppButton.vue'
import PriorityBadge from '@/shared/ui/PriorityBadge.vue'
import StatusChip from '@/shared/ui/StatusChip.vue'
import TubeDot from '@/shared/ui/TubeDot.vue'

const props = defineProps<{ row: QueueRow }>()

const router = useRouter()
const store = useQueueStore()
const { isCallingNext } = storeToRefs(store)
const { callNext } = store

const canCall = computed(() => props.row.order.status === 'Waiting' && props.row.position === 1)
const positionLabel = computed(() =>
  props.row.position ? String(props.row.position).padStart(2, '0') : '—',
)
const shortId = computed(() => props.row.order.id.slice(0, 8))

const goDetail = () => router.push(`/orders/${props.row.order.id}`)
const goReview = () => router.push(`/orders/${props.row.order.id}/samples/review`)
</script>

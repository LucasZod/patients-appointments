<template>
  <tr :class="rowClass">
    <td class="px-3 py-2 text-sm font-medium text-text">{{ positionLabel }}</td>
    <td class="px-3 py-2">
      <div class="flex flex-col">
        <span class="text-sm font-semibold text-text">{{ row.order.patientName ?? '—' }}</span>
        <span class="text-xs text-secondary">OS {{ shortId }}</span>
      </div>
    </td>
    <td class="px-3 py-2">
      <PriorityBadge :priority="row.order.priority" />
    </td>
    <td class="px-3 py-2 text-sm text-text">{{ row.waitingMinutes }} min</td>
    <td class="px-3 py-2">
      <div class="flex items-center gap-1">
        <TubeDot v-for="tube in row.tubeTypes" :key="tube" :tube-type="tube" />
      </div>
    </td>
    <td class="px-3 py-2">
      <StatusChip :status="row.order.status" />
    </td>
    <td class="px-3 py-2 text-right">
      <AppButton
        v-if="canCall"
        variant="primary"
        :loading="isCallingNext"
        @click="callNext"
      >
        Chamar
      </AppButton>
      <AppButton v-else-if="row.order.status === 'InProgress'" variant="outlined" @click="goDetail">
        Ver Detalhes
      </AppButton>
      <AppButton v-else-if="row.order.status === 'Collected'" variant="outlined" @click="goReview">
        Conferir
      </AppButton>
      <span v-else-if="row.order.status === 'Waiting'" class="text-sm text-secondary">Na fila</span>
      <span v-else class="text-sm text-secondary">—</span>
    </td>
  </tr>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { storeToRefs } from 'pinia'
import { useQueueStore, type QueueRow } from '@/stores/queue.store'
import PriorityBadge from '@/shared/ui/PriorityBadge.vue'
import StatusChip from '@/shared/ui/StatusChip.vue'
import TubeDot from '@/shared/ui/TubeDot.vue'
import AppButton from '@/shared/ui/AppButton.vue'

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

const rowClass = computed(() =>
  props.row.order.status === 'InProgress'
    ? 'border-l-4 border-warning bg-warning/5'
    : 'border-b border-border',
)

const goDetail = () => router.push(`/orders/${props.row.order.id}`)
const goReview = () => router.push(`/orders/${props.row.order.id}/samples/review`)
</script>

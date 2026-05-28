<template>
  <AppCard>
    <ol class="flex items-center gap-0">
      <li
        v-for="(step, i) in steps"
        :key="step.status"
        class="flex flex-1 items-center"
      >
        <div class="flex flex-col items-center gap-1 flex-1">
          <div
            :class="[
              'w-8 h-8 rounded-full flex items-center justify-center text-xs font-bold border-2 transition-colors',
              stepState(step.status) === 'done'
                ? 'bg-primary border-primary text-white'
                : stepState(step.status) === 'active'
                  ? 'bg-primary/15 border-primary text-primary'
                  : 'bg-surface border-border text-secondary',
            ]"
          >
            {{ i + 1 }}
          </div>
          <span
            :class="[
              'text-xs font-medium text-center',
              stepState(step.status) === 'done' || stepState(step.status) === 'active'
                ? 'text-primary'
                : 'text-secondary',
            ]"
          >
            {{ step.label }}
          </span>
        </div>
        <div
          v-if="i < steps.length - 1"
          :class="[
            'h-0.5 flex-1 -mt-5 transition-colors',
            stepState(step.status) === 'done' ? 'bg-primary' : 'bg-border',
          ]"
        />
      </li>
    </ol>
  </AppCard>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppCard from '@/shared/ui/AppCard.vue'
import type { ServiceOrderStatus } from '@/types'

const { currentOrder } = storeToRefs(useServiceOrderStore())

const ORDER: ServiceOrderStatus[] = ['Waiting', 'InProgress', 'Collected', 'Completed']

const steps = computed(() => {
  const status = currentOrder.value?.status
  const isRejected = status === 'Rejected'
  return [
    { status: 'Waiting' as ServiceOrderStatus, label: 'Aguardando' },
    { status: 'InProgress' as ServiceOrderStatus, label: 'Em Coleta' },
    { status: 'Collected' as ServiceOrderStatus, label: 'Coletado' },
    { status: (isRejected ? 'Rejected' : 'Completed') as ServiceOrderStatus, label: isRejected ? 'Rejeitado' : 'Concluído' },
  ]
})

const stepState = (stepStatus: ServiceOrderStatus): 'done' | 'active' | 'idle' => {
  const current = currentOrder.value?.status
  if (!current) return 'idle'

  if (current === 'Rejected') {
    if (stepStatus === 'Rejected') return 'active'
    const idx = ORDER.indexOf(stepStatus)
    const rejIdx = ORDER.indexOf('Collected')
    return idx <= rejIdx ? 'done' : 'idle'
  }

  const currentIdx = ORDER.indexOf(current)
  const stepIdx = ORDER.indexOf(stepStatus)
  if (stepIdx < currentIdx) return 'done'
  if (stepIdx === currentIdx) return 'active'
  return 'idle'
}
</script>

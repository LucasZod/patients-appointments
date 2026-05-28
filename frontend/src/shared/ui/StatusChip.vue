<template>
  <AppBadge :variant="variant">{{ label }}</AppBadge>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import AppBadge from './AppBadge.vue'
import type { OrderItemStatus, SampleStatus, ServiceOrderStatus } from '@/types'

type StatusValue = ServiceOrderStatus | SampleStatus | OrderItemStatus

const props = defineProps<{ status: StatusValue }>()

const variant = computed(() => {
  switch (props.status) {
    case 'Waiting':
      return 'waiting' as const
    case 'InProgress':
      return 'in-progress' as const
    case 'Collected':
      return 'collected' as const
    case 'Completed':
    case 'Approved':
      return 'completed' as const
    case 'Rejected':
      return 'rejected' as const
    case 'Pending':
    default:
      return 'neutral' as const
  }
})

const label = computed(() => {
  switch (props.status) {
    case 'Waiting':
      return 'Aguardando'
    case 'InProgress':
      return 'Em Coleta'
    case 'Collected':
      return 'Coletado'
    case 'Completed':
      return 'Concluído'
    case 'Rejected':
      return 'Rejeitado'
    case 'Approved':
      return 'Aprovado'
    case 'Pending':
      return 'Pendente'
    default:
      return String(props.status)
  }
})
</script>

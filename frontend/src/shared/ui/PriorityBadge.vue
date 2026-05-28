<template>
  <AppBadge :variant="variant">{{ label }}</AppBadge>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import AppBadge from './AppBadge.vue'
import type { Priority } from '@/types'

const props = defineProps<{ priority: Priority }>()

const variant = computed(() => {
  switch (props.priority) {
    case 'Urgent':
      return 'urgent' as const
    case 'Preferred':
      return 'preferred' as const
    case 'Normal':
    default:
      return 'normal' as const
  }
})

const label = computed(() => {
  switch (props.priority) {
    case 'Urgent':
      return 'Urgente'
    case 'Preferred':
      return 'Preferencial'
    case 'Normal':
    default:
      return 'Normal'
  }
})
</script>

<template>
  <span
    :class="[
      'inline-flex items-center rounded-badge px-2.5 py-0.5 text-xs font-semibold whitespace-nowrap',
      variantClass,
    ]"
  >
    <slot />
  </span>
</template>

<script setup lang="ts">
import { computed } from 'vue'

type Variant =
  | 'urgent'
  | 'preferred'
  | 'normal'
  | 'waiting'
  | 'in-progress'
  | 'collected'
  | 'completed'
  | 'rejected'
  | 'neutral'

const props = withDefaults(defineProps<{ variant?: Variant }>(), { variant: 'neutral' })

const variantClass = computed(() => {
  switch (props.variant) {
    case 'urgent':
      return 'bg-error/10 text-error'
    case 'preferred':
      return 'bg-warning/10 text-warning'
    case 'normal':
      return 'bg-secondary/15 text-primary'
    case 'waiting':
      return 'bg-primary/8 text-primary'
    case 'in-progress':
      return 'bg-warning/15 text-warning'
    case 'collected':
      return 'bg-accent/30 text-primary'
    case 'completed':
      return 'bg-success/15 text-success'
    case 'rejected':
      return 'bg-error/15 text-error'
    case 'neutral':
    default:
      return 'bg-border text-text'
  }
})
</script>

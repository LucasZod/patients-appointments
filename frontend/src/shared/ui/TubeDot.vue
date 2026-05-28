<template>
  <span
    :class="['inline-block rounded-full', colorClass]"
    :style="{ width: `${size}px`, height: `${size}px` }"
    :title="tubeType"
    aria-hidden="true"
  />
</template>

<script setup lang="ts">
import { computed } from 'vue'

const props = withDefaults(
  defineProps<{
    tubeType: string
    size?: number
  }>(),
  { size: 10 },
)

const colorClass = computed(() => {
  switch (props.tubeType) {
    case 'purple':
      return 'bg-tube-purple'
    case 'yellow':
      return 'bg-tube-yellow'
    case 'green':
      return 'bg-tube-green'
    case 'urine':
      return 'bg-tube-urine'
    default:
      return 'bg-border'
  }
})
</script>

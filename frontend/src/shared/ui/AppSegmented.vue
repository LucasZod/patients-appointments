<template>
  <div class="inline-flex gap-1" role="group">
    <button
      v-for="option in options"
      :key="option.value"
      type="button"
      :aria-pressed="option.value === modelValue"
      :class="[base, shapeClass, option.value === modelValue ? activeClass : inactiveClass]"
      @click="$emit('update:modelValue', option.value)"
    >
      {{ option.label }}
    </button>
  </div>
</template>

<script setup lang="ts" generic="T extends string">
import { computed } from 'vue'

interface Option {
  value: T
  label: string
}

const props = withDefaults(
  defineProps<{
    options: ReadonlyArray<Option>
    modelValue: T
    shape?: 'pill' | 'tab'
  }>(),
  { shape: 'pill' },
)

defineEmits<{ 'update:modelValue': [value: T] }>()

const base = 'cursor-pointer px-4 py-1.5 text-sm font-medium transition-colors border'

const shapeClass = computed(() => (props.shape === 'pill' ? 'rounded-badge' : 'rounded-button'))

const activeClass = 'border-primary bg-primary/80 text-white'
const inactiveClass = 'border-border bg-surface text-text hover:bg-bg'
</script>

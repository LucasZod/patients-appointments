<template>
  <button
    :type="type"
    :disabled="disabled || loading"
    :aria-busy="loading"
    :class="[base, variantClass, fullWidth ? 'w-full' : '']"
    v-bind="$attrs"
  >
    <AppSpinner v-if="loading" :size="14" />
    <slot v-else />
  </button>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import AppSpinner from './AppSpinner.vue'

interface Props {
  variant?: 'primary' | 'outlined' | 'danger' | 'ghost'
  loading?: boolean
  disabled?: boolean
  fullWidth?: boolean
  type?: 'button' | 'submit' | 'reset'
}

const props = withDefaults(defineProps<Props>(), {
  variant: 'primary',
  loading: false,
  disabled: false,
  fullWidth: false,
  type: 'button',
})

const base =
  'inline-flex items-center justify-center gap-2 rounded-button px-4 py-2 text-sm font-medium transition-colors disabled:cursor-not-allowed disabled:opacity-50 cursor-pointer'

const variantClass = computed(() => {
  switch (props.variant) {
    case 'outlined':
      return 'border border-primary text-primary bg-transparent hover:bg-primary/5'
    case 'danger':
      return 'bg-error text-white hover:bg-error/90'
    case 'ghost':
      return 'text-primary bg-transparent hover:bg-primary/5'
    case 'primary':
    default:
      return 'bg-primary text-white hover:bg-primary/90'
  }
})
</script>

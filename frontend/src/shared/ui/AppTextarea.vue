<template>
  <label class="flex flex-col gap-1">
    <span v-if="label" class="text-sm font-medium text-text">{{ label }}</span>
    <textarea
      :value="modelValue"
      :placeholder="placeholder"
      :disabled="disabled"
      :rows="rows"
      :aria-invalid="!!error"
      :class="[
        'resize-y rounded-button border bg-surface px-3 py-2 text-sm outline-none transition-colors',
        error
          ? 'border-error focus:border-error'
          : 'border-border focus:border-accent',
        disabled ? 'cursor-not-allowed opacity-50' : '',
      ]"
      v-bind="$attrs"
      @input="onInput"
      @blur="$emit('blur')"
    />
    <span v-if="error" class="text-xs text-error">{{ error }}</span>
  </label>
</template>

<script setup lang="ts">
defineOptions({ inheritAttrs: false })

interface Props {
  label?: string
  modelValue?: string
  error?: string
  placeholder?: string
  disabled?: boolean
  rows?: number
}

withDefaults(defineProps<Props>(), {
  disabled: false,
  rows: 3,
})

const emit = defineEmits<{
  'update:modelValue': [value: string]
  blur: []
}>()

const onInput = (event: Event) => {
  emit('update:modelValue', (event.target as HTMLTextAreaElement).value)
}
</script>

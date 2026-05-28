<template>
  <label class="flex flex-col gap-1">
    <span v-if="label" class="text-sm font-medium text-text">{{ label }}</span>
    <input
      :value="modelValue"
      :type="type"
      :placeholder="placeholder"
      :disabled="disabled"
      :aria-invalid="!!error"
      :class="[
        'rounded-button border bg-surface px-3 py-2 text-sm outline-none transition-colors',
        error
          ? 'border-error focus:border-error'
          : 'border-border focus:border-primary',
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
  type?: string
  mask?: 'cpf' | 'phone'
}

const props = withDefaults(defineProps<Props>(), {
  type: 'text',
  disabled: false,
})

const emit = defineEmits<{
  'update:modelValue': [value: string]
  blur: []
}>()

const applyCpfMask = (value: string): string => {
  const digits = value.replace(/\D/g, '').slice(0, 11)
  return digits
    .replace(/^(\d{3})(\d)/, '$1.$2')
    .replace(/^(\d{3})\.(\d{3})(\d)/, '$1.$2.$3')
    .replace(/\.(\d{3})(\d)/, '.$1-$2')
}

const applyPhoneMask = (value: string): string => {
  const digits = value.replace(/\D/g, '').slice(0, 11)
  if (digits.length <= 10) {
    return digits.replace(/^(\d{2})(\d{4})(\d)/, '($1) $2-$3').replace(/^(\d{2})(\d)/, '($1) $2')
  }
  return digits.replace(/^(\d{2})(\d{5})(\d)/, '($1) $2-$3').replace(/^(\d{2})(\d)/, '($1) $2')
}

const onInput = (event: Event) => {
  const target = event.target as HTMLInputElement
  let value = target.value
  if (props.mask === 'cpf') value = applyCpfMask(value)
  if (props.mask === 'phone') value = applyPhoneMask(value)
  if (value !== target.value) target.value = value
  emit('update:modelValue', value)
}
</script>

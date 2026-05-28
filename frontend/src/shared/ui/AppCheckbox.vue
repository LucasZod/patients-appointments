<template>
  <input
    type="checkbox"
    class="h-4 w-4 cursor-pointer accent-primary"
    :checked="modelValue"
    :disabled="disabled"
    v-bind="$attrs"
    @change="$emit('update:modelValue', ($event.target as HTMLInputElement).checked)"
  />
</template>

<script setup lang="ts">
defineOptions({ inheritAttrs: false })

withDefaults(defineProps<{ modelValue: boolean; disabled?: boolean }>(), { disabled: false })

defineEmits<{ 'update:modelValue': [value: boolean] }>()
</script>

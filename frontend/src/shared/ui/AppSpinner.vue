<template>
  <span
    class="inline-block animate-spin rounded-full border-3 border-current border-t-transparent"
    :style="{ width: `${size}px`, height: `${size}px` }"
    role="status"
    aria-label="Carregando"
  />
</template>

<script setup lang="ts">
withDefaults(defineProps<{ size?: number }>(), { size: 40 })
</script>

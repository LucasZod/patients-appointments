<template>
  <div class="flex h-full items-center justify-center text-secondary">
    <p>{{ message }}</p>
  </div>
</template>

<script setup lang="ts">
withDefaults(defineProps<{ message?: string }>(), {
  message: 'Em construção — vamos implementar nas próximas etapas.',
})
</script>

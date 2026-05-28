<template>
  <section
    :class="[
      'rounded-card border border-border bg-surface',
      padding === 'lg' ? 'p-6' : padding === 'sm' ? 'p-3' : 'p-4',
    ]"
  >
    <slot />
  </section>
</template>

<script setup lang="ts">
withDefaults(defineProps<{ padding?: 'sm' | 'md' | 'lg' }>(), { padding: 'md' })
</script>

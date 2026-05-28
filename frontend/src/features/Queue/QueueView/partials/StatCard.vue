<template>
  <AppCard>
    <div :class="['flex flex-col gap-1 border-l-4 pl-3', accentClass]">
      <span class="text-xs font-medium text-secondary uppercase">{{ label }}</span>
      <span class="text-2xl font-semibold text-text">{{ value }}</span>
    </div>
  </AppCard>
</template>

<script setup lang="ts">
import AppCard from '@/shared/ui/AppCard.vue'

defineProps<{ label: string; value: number; accentClass: string }>()
</script>

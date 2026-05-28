<template>
  <Teleport to="body">
    <div
      v-if="open"
      class="fixed inset-0 z-50 flex items-center justify-center bg-[rgba(18,33,41,0.5)]"
      role="dialog"
      aria-modal="true"
      @click.self="$emit('close')"
    >
      <div class="w-[480px] max-w-[calc(100vw-2rem)] rounded-card bg-surface shadow-xl">
        <header class="flex items-center justify-between border-b border-border px-5 py-3">
          <h2 class="text-base font-semibold text-text">{{ title }}</h2>
          <AppButton
            class="rounded p-1 text-text/60 hover:bg-bg hover:text-text"
            variant="ghost"
            aria-label="Fechar"
            @click="$emit('close')"
          >
            ✕
          </AppButton>
        </header>
        <div class="px-5 py-4">
          <slot />
        </div>
      </div>
    </div>
  </Teleport>
</template>

<script setup lang="ts">
import AppButton from '@/shared/ui/AppButton.vue'
defineProps<{ open: boolean; title: string }>()

defineEmits<{ close: [] }>()
</script>

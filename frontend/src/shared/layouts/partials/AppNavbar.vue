<template>
  <header
    class="flex h-14 items-center justify-between border-b border-border bg-primary px-6 text-white"
  >
    <div class="flex items-baseline gap-2">
      <span class="text-base font-bold">Lab System</span>
      <span class="text-xs text-secondary">Ocean Health</span>
    </div>

    <nav class="flex items-center gap-6 text-sm">
      <RouterLink
        v-for="link in links"
        :key="link.to"
        :to="link.to"
        class="text-white/80 hover:text-white"
        active-class="text-white border-b-2 border-accent pb-1"
      >
        {{ link.label }}
      </RouterLink>
    </nav>

    <div class="flex items-center gap-3">
      <button
        type="button"
        class="rounded p-1 text-white/80 hover:bg-white/10 hover:text-white"
        aria-label="Notificações"
      >
        🔔
      </button>
      <button
        type="button"
        class="rounded p-1 text-white/80 hover:bg-white/10 hover:text-white"
        aria-label="Configurações"
      >
        ⚙
      </button>
    </div>
  </header>
</template>

<script setup lang="ts">
import { RouterLink } from 'vue-router'

const links = [
  { to: '/orders/new', label: 'Ordens' },
  { to: '/queue', label: 'Fila' },
]
</script>

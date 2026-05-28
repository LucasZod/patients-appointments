<template>
  <header
    class="flex h-14 items-center justify-between border-b border-border bg-primary px-6 text-white"
  >
    <div class="flex items-center gap-3 justify-between w-full">
      <RouterLink to="/" class="text-base font-bold flex items-center gap-2">
        <img src="/ocean-health.png" alt="Logo" class="h-6 w-6" />
        <span class="hidden sm:inline">Ocean Health</span>
      </RouterLink>
      <nav class="flex gap-4 md:hidden">
        <RouterLink
          v-for="link in links"
          :key="link.to"
          :to="link.to"
          class="flex items-center gap-2 text-sm hover:bg-bg text-white/50"
          active-class="text-white! font-medium!"
        >
          {{ link.label }}
        </RouterLink>
      </nav>
    </div>
  </header>
</template>

<script setup lang="ts">
const links = [
  { to: '/orders/new', label: 'Nova Ordem' },
  { to: '/queue', label: 'Fila' },
  { to: '/reports', label: 'Relatórios' },
]
</script>

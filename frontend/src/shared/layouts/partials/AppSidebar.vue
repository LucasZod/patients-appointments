<template>
  <aside
    class="w-55 flex-col border-r border-border bg-surface p-4 hidden md:flex"
    aria-label="Navegação lateral"
  >
    <nav class="flex flex-1 flex-col gap-1">
      <RouterLink
        v-for="link in links"
        :key="link.to"
        :to="link.to"
        class="flex items-center gap-2 rounded-button px-3 py-2 text-sm text-text hover:bg-bg"
        active-class="bg-bg font-medium"
      >
        {{ link.label }}
      </RouterLink>
    </nav>
  </aside>
</template>

<script setup lang="ts">
import { RouterLink } from 'vue-router'

const links = [
  { to: '/orders/new', label: 'Nova Ordem' },
  { to: '/queue', label: 'Fila' },
  { to: '/reports', label: 'Relatórios' },
]
</script>

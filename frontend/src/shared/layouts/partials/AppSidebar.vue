<template>
  <aside
    class="flex w-[220px] flex-col border-r border-border bg-surface p-4"
    aria-label="Navegação lateral"
  >
    <RouterLink
      to="/orders/new"
      class="mb-4 inline-flex items-center justify-center rounded-button bg-primary px-3 py-2 text-sm font-medium text-white hover:bg-primary/90"
    >
      + Nova Ordem
    </RouterLink>

    <nav class="flex flex-1 flex-col gap-1">
      <RouterLink
        v-for="link in links"
        :key="link.to"
        :to="link.to"
        class="flex items-center gap-2 rounded-button px-3 py-2 text-sm text-text hover:bg-bg"
        active-class="border-l-[3px] border-primary bg-bg font-medium"
      >
        <span aria-hidden="true">{{ link.icon }}</span>
        {{ link.label }}
      </RouterLink>
    </nav>

    <div class="mt-auto flex flex-col gap-1 border-t border-border pt-4 text-sm text-secondary">
      <button type="button" class="text-left hover:text-text">Suporte</button>
      <button type="button" class="text-left hover:text-text">Sair</button>
    </div>
  </aside>
</template>

<script setup lang="ts">
import { RouterLink } from 'vue-router'

const links = [
  { to: '/orders/new', label: 'Nova Ordem', icon: '📋' },
  { to: '/queue', label: 'Fila', icon: '👥' },
  { to: '/reports', label: 'Relatórios', icon: '📊' },
]
</script>

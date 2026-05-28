<template>
  <AppCard>
    <h2 class="text-sm font-semibold text-text mb-3">Informações</h2>
    <dl class="flex flex-col gap-2">
      <div class="flex justify-between text-sm">
        <dt class="text-secondary">Criado em</dt>
        <dd class="font-medium text-text">{{ formatDate(currentOrder!.createdAt) }}</dd>
      </div>
      <div class="flex justify-between text-sm">
        <dt class="text-secondary">Chamado em</dt>
        <dd class="font-medium text-text">{{ currentOrder!.calledAt ? formatDate(currentOrder!.calledAt) : '—' }}</dd>
      </div>
      <div class="flex justify-between text-sm">
        <dt class="text-secondary">Finalizado em</dt>
        <dd class="font-medium text-text">{{ currentOrder!.finishedAt ? formatDate(currentOrder!.finishedAt) : '—' }}</dd>
      </div>
      <div class="flex justify-between text-sm">
        <dt class="text-secondary">Total de exames</dt>
        <dd class="font-medium text-text">{{ currentOrder!.items.length }}</dd>
      </div>
    </dl>
  </AppCard>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppCard from '@/shared/ui/AppCard.vue'

const { currentOrder } = storeToRefs(useServiceOrderStore())

const formatDate = (iso: string) =>
  new Date(iso).toLocaleString('pt-BR', { dateStyle: 'short', timeStyle: 'short' })
</script>

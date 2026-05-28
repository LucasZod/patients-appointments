<template>
  <AppCard padding="sm">
    <table class="w-full border-collapse">
      <thead>
        <tr class="border-b border-border text-left text-xs text-secondary uppercase">
          <th class="px-3 py-2 font-medium">Pos.</th>
          <th class="px-3 py-2 font-medium">Paciente</th>
          <th class="px-3 py-2 font-medium">Prioridade</th>
          <th class="px-3 py-2 font-medium">Espera</th>
          <th class="px-3 py-2 font-medium">Exames</th>
          <th class="px-3 py-2 font-medium">Status</th>
          <th class="px-3 py-2 text-right font-medium">Ação</th>
        </tr>
      </thead>
      <tbody>
        <QueueRow v-for="row in filtered" :key="row.order.id" :row="row" />
      </tbody>
    </table>
  </AppCard>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useQueueStore } from '@/stores/queue.store'
import AppCard from '@/shared/ui/AppCard.vue'
import QueueRow from './QueueRow.vue'

const { filtered } = storeToRefs(useQueueStore())
</script>

<template>
  <div class="grid grid-cols-2 md:gap-4 gap-2 md:grid-cols-4">
    <StatCard label="Aguardando" :value="stats.waiting" accent-class="border-primary" />
    <StatCard label="Em Coleta" :value="stats.inProgress" accent-class="border-warning" />
    <StatCard label="Concluídos Hoje" :value="stats.completedToday" accent-class="border-success" />
    <StatCard label="Rejeitados Hoje" :value="stats.rejectedToday" accent-class="border-error" />
  </div>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useQueueStore } from '@/stores/queue.store'
import StatCard from './StatCard.vue'

const { stats } = storeToRefs(useQueueStore())
</script>

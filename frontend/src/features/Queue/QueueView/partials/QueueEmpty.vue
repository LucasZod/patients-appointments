<template>
  <AppCard padding="lg">
    <div class="flex flex-col items-center gap-3 py-10 text-center">
      <p class="text-sm text-secondary">{{ message }}</p>
    </div>
  </AppCard>
</template>

<script setup lang="ts">
import AppCard from '@/shared/ui/AppCard.vue'
import { useQueueStore } from '@/stores/queue.store'
import { storeToRefs } from 'pinia'
import { computed } from 'vue'
const store = useQueueStore()
const { filterStatus } = storeToRefs(store)

const message = computed(() => {
  switch (filterStatus.value) {
    case 'Waiting':
      return 'Nenhum paciente aguardando atendimento.'
    case 'InProgress':
      return 'Nenhum paciente em coleta.'
    case 'Collected':
      return 'Nenhum paciente coletado.'
    default:
      return 'Nenhum atendimento na fila.'
  }
})
</script>

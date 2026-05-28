<template>
  <AppCard>
    <div class="flex items-center gap-3 flex-col md:flex-row">
      <AppInput
        classLabel="w-full"
        type="search"
        placeholder="Buscar paciente..."
        aria-label="Buscar paciente"
        :model-value="searchTerm"
        @update:model-value="setSearch"
      />

      <AppSegmented
        class="flex-col md:flex-row w-full"
        shape="tab"
        :options="tabs"
        :model-value="filterStatus"
        @update:model-value="setFilter"
      />

      <AppButton
        variant="primary"
        class="md:max-w-50 w-full"
        :loading="isCallingNext"
        @click="callNext"
      >
        Chamar Próximo
      </AppButton>
    </div>
  </AppCard>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useQueueStore, type QueueFilter } from '@/stores/queue.store'
import AppCard from '@/shared/ui/AppCard.vue'
import AppInput from '@/shared/ui/AppInput.vue'
import AppButton from '@/shared/ui/AppButton.vue'
import AppSegmented from '@/shared/ui/AppSegmented.vue'

const store = useQueueStore()
const { filterStatus, searchTerm, isCallingNext } = storeToRefs(store)
const { setFilter, setSearch, callNext } = store

const tabs: Array<{ value: QueueFilter; label: string }> = [
  { value: 'all', label: 'Todos' },
  { value: 'Waiting', label: 'Aguardando' },
  { value: 'InProgress', label: 'Em Coleta' },
  { value: 'Collected', label: 'Coletado' },
]
</script>

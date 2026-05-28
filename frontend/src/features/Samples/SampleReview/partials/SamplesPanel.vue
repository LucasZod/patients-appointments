<template>
  <AppCard>
    <h2 class="mb-3 text-sm font-semibold text-text">Amostras Coletadas</h2>
    <p v-if="!samples.length" class="py-4 text-center text-sm text-secondary">
      Nenhuma amostra registrada para esta ordem.
    </p>
    <ul v-else class="flex flex-col gap-3">
      <SampleCard v-for="sample in samples" :key="sample.id" :sample="sample" />
    </ul>
  </AppCard>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useSampleStore } from '@/stores/sample.store'
import AppCard from '@/shared/ui/AppCard.vue'
import SampleCard from './SampleCard.vue'

const { samples } = storeToRefs(useSampleStore())
</script>

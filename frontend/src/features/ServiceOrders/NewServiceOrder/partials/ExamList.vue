<template>
  <AppCard>
    <div class="flex flex-col gap-3">
      <div class="flex items-baseline justify-between">
        <h2 class="text-sm font-semibold text-secondary uppercase">Exames Solicitados</h2>
        <span class="text-sm text-secondary">{{ selectedExamCodes.length }} selecionados</span>
      </div>
      <div class="grid grid-cols-1 gap-2 md:grid-cols-2">
        <ExamItem v-for="exam in examsCatalog" :key="exam.code" :exam="exam" />
      </div>
    </div>
  </AppCard>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import { examsCatalog } from '@/data/exams-catalog'
import AppCard from '@/shared/ui/AppCard.vue'
import ExamItem from './ExamItem.vue'

const { selectedExamCodes } = storeToRefs(useServiceOrderStore())
</script>

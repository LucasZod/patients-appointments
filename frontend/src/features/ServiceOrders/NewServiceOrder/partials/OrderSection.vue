<template>
  <section v-if="selectedPatient" class="flex flex-col gap-4">
    <PatientCard />
    <PrioritySelector />
    <ExamList />
    <TubeSummary />
    <SubmitButton />
  </section>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import PatientCard from './PatientCard.vue'
import PrioritySelector from './PrioritySelector.vue'
import ExamList from './ExamList.vue'
import TubeSummary from './TubeSummary.vue'
import SubmitButton from './SubmitButton.vue'

const { selectedPatient } = storeToRefs(useServiceOrderStore())
</script>

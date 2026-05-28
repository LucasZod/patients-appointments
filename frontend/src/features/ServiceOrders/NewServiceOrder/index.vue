<template>
  <div class="flex flex-col gap-6">
    <PageHeader />
    <PatientSearch />
    <RegisterPatientModal />
    <OrderSection />
  </div>
</template>

<script setup lang="ts">
import PageHeader from './partials/PageHeader.vue'
import PatientSearch from './partials/PatientSearch.vue'
import OrderSection from './partials/OrderSection.vue'
import RegisterPatientModal from '@/features/Patients/RegisterPatientModal/index.vue'
</script>

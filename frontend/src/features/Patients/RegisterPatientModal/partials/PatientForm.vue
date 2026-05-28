<template>
  <form class="flex flex-col gap-5" @submit.prevent="registerPatient">
    <NameField />
    <CpfField />
    <BirthDateField />
    <PhoneField />
    <ErrorBanner />
    <FormActions />
  </form>
</template>

<script setup lang="ts">
import { usePatientStore } from '@/stores/patient.store'
import NameField from './NameField.vue'
import CpfField from './CpfField.vue'
import BirthDateField from './BirthDateField.vue'
import PhoneField from './PhoneField.vue'
import ErrorBanner from './ErrorBanner.vue'
import FormActions from './FormActions.vue'

const { registerPatient } = usePatientStore()
</script>

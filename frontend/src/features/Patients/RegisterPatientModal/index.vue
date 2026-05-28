<template>
  <AppModal :open="showRegisterModal" title="Cadastrar Paciente" @close="closeRegisterModal">
    <PatientForm />
  </AppModal>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { usePatientStore } from '@/stores/patient.store'
import AppModal from '@/shared/ui/AppModal.vue'
import PatientForm from './partials/PatientForm.vue'

const store = usePatientStore()
const { showRegisterModal } = storeToRefs(store)
const { closeRegisterModal } = store
</script>

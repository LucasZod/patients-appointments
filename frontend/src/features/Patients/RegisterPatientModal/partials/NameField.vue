<template>
  <AppInput
    label="Nome completo"
    placeholder="Maria Silva"
    :model-value="registerForm.name"
    :error="validationErrors.name"
    :disabled="isRegistering"
    @update:model-value="setRegisterField('name', $event)"
  />
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { usePatientStore } from '@/stores/patient.store'
import AppInput from '@/shared/ui/AppInput.vue'

const store = usePatientStore()
const { registerForm, validationErrors, isRegistering } = storeToRefs(store)
const { setRegisterField } = store
</script>

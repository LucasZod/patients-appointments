<template>
  <AppInput
    label="Data de nascimento"
    type="date"
    min="1900-01-01"
    :max="today"
    :model-value="registerForm.birthDate"
    :error="validationErrors.birthDate"
    :disabled="isRegistering"
    @update:model-value="setRegisterField('birthDate', $event)"
  />
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { usePatientStore } from '@/stores/patient.store'
import AppInput from '@/shared/ui/AppInput.vue'

const store = usePatientStore()
const { registerForm, validationErrors, isRegistering } = storeToRefs(store)
const { setRegisterField } = store

const today = new Date().toISOString().slice(0, 10)
</script>

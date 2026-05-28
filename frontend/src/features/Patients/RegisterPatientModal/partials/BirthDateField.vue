<template>
  <AppInput
    label="Data de nascimento"
    type="date"
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
</script>

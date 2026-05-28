<template>
  <AppInput
    label="CPF"
    placeholder="000.000.000-00"
    mask="cpf"
    :model-value="registerForm.cpf"
    :error="validationErrors.cpf"
    :disabled="isRegistering"
    @update:model-value="onUpdate"
  />
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { usePatientStore } from '@/stores/patient.store'
import AppInput from '@/shared/ui/AppInput.vue'

const store = usePatientStore()
const { registerForm, validationErrors, isRegistering } = storeToRefs(store)
const { setRegisterField } = store

const onUpdate = (masked: string) => setRegisterField('cpf', masked.replace(/\D/g, ''))
</script>

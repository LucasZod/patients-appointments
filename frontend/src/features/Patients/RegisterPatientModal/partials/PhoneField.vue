<template>
  <AppInput
    label="Telefone (opcional)"
    placeholder="(11) 99999-9999"
    mask="phone"
    :model-value="registerForm.phone"
    :error="validationErrors.phone"
    :disabled="isRegistering"
    @update:model-value="setRegisterField('phone', $event)"
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

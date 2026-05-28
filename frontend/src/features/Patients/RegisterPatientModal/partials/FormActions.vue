<template>
  <div class="flex justify-end gap-2 pt-2">
    <AppButton variant="ghost" :disabled="isRegistering" @click="closeRegisterModal">
      Cancelar
    </AppButton>
    <AppButton type="submit" variant="primary" :loading="isRegistering">
      Salvar Paciente
    </AppButton>
  </div>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { usePatientStore } from '@/stores/patient.store'
import AppButton from '@/shared/ui/AppButton.vue'

const store = usePatientStore()
const { isRegistering } = storeToRefs(store)
const { closeRegisterModal } = store
</script>

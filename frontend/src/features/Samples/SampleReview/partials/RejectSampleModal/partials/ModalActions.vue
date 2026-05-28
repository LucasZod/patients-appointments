<template>
  <div class="mt-4 flex justify-end gap-3">
    <AppButton variant="outlined" @click="closeRejectModal">Cancelar</AppButton>
    <AppButton variant="danger" :loading="isSubmitting" @click="rejectSample">
      Confirmar Rejeição
    </AppButton>
  </div>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useSampleStore } from '@/stores/sample.store'
import AppButton from '@/shared/ui/AppButton.vue'

const store = useSampleStore()
const { isSubmitting } = storeToRefs(store)
const { closeRejectModal, rejectSample } = store
</script>

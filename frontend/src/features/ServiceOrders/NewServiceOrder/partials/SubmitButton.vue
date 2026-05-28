<template>
  <AppButton
    type="button"
    variant="primary"
    :full-width="true"
    :disabled="!canSubmit"
    :loading="isCreating"
    @click="createOrder"
  >
    Registrar Chegada
  </AppButton>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppButton from '@/shared/ui/AppButton.vue'

const store = useServiceOrderStore()
const { canSubmit, isCreating } = storeToRefs(store)
const { createOrder } = store
</script>

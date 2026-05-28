<template>
  <div class="flex flex-col gap-2">
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
    <p v-if="createError" class="text-sm text-error">{{ createError }}</p>
  </div>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppButton from '@/shared/ui/AppButton.vue'

const store = useServiceOrderStore()
const { canSubmit, isCreating, createError } = storeToRefs(store)
const { createOrder } = store
</script>

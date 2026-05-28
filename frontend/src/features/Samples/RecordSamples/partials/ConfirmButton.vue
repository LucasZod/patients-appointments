<template>
  <AppButton variant="primary" class="w-full" :loading="isSubmitting" @click="onConfirm">
    Confirmar Coleta
  </AppButton>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useSampleStore } from '@/stores/sample.store'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppButton from '@/shared/ui/AppButton.vue'

const sampleStore = useSampleStore()
const { isSubmitting, tubeQuantities } = storeToRefs(sampleStore)
const { recordSamples } = sampleStore

const { currentOrder } = storeToRefs(useServiceOrderStore())

const onConfirm = () => {
  const order = currentOrder.value
  if (!order) return
  const tubes = Object.entries(tubeQuantities.value).flatMap(([type, qty]) =>
    Array<string>(qty).fill(type),
  )
  recordSamples(order.id, tubes)
}
</script>

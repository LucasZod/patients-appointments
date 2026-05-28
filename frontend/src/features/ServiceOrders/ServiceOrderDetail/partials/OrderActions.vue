<template>
  <AppCard v-if="currentOrder!.status === 'InProgress'">
    <AppButton variant="primary" class="w-full" @click="goRecord">Registrar Coleta</AppButton>
  </AppCard>
  <AppCard v-else-if="currentOrder!.status === 'Collected'">
    <AppButton variant="outlined" class="w-full" @click="goReview">Conferir Amostras</AppButton>
  </AppCard>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppCard from '@/shared/ui/AppCard.vue'
import AppButton from '@/shared/ui/AppButton.vue'

const router = useRouter()
const { currentOrder } = storeToRefs(useServiceOrderStore())

const goRecord = () => router.push(`/orders/${currentOrder.value!.id}/samples/record`)
const goReview = () => router.push(`/orders/${currentOrder.value!.id}/samples/review`)
</script>

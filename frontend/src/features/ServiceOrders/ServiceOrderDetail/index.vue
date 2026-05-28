<template>
  <div v-if="isLoadingOrder" class="flex items-center justify-center py-20">
    <AppSpinner />
  </div>

  <div v-else-if="loadError" class="flex items-center justify-center py-20">
    <p class="text-sm text-error">{{ loadError }}</p>
  </div>

  <div v-else-if="currentOrder" class="flex flex-col gap-6">
    <OrderHeader />
    <!-- <StatusStepper /> -->
    <div class="grid gap-6 md:grid-cols-3">
      <div class="md:col-span-2">
        <OrderItemsList />
      </div>
      <div class="flex flex-col gap-4">
        <OrderInfo />
        <OrderActions />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppSpinner from '@/shared/ui/AppSpinner.vue'
import OrderHeader from './partials/OrderHeader.vue'
import StatusStepper from './partials/StatusStepper.vue'
import OrderItemsList from './partials/OrderItemsList.vue'
import OrderInfo from './partials/OrderInfo.vue'
import OrderActions from './partials/OrderActions.vue'

const route = useRoute()
const store = useServiceOrderStore()
const { currentOrder, isLoadingOrder, loadError } = storeToRefs(store)
const { loadOrder } = store

onMounted(() => loadOrder(route.params.id as string))
</script>

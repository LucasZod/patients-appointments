<template>
  <ReviewPageHeader />
  <div class="grid grid-cols-1 gap-6 md:grid-cols-2">
    <OrderItemsPanel />
    <SamplesPanel />
  </div>
  <ReviewProgress />
  <RejectSampleModal />
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { useSampleStore } from '@/stores/sample.store'
import { useServiceOrderStore } from '@/stores/service-order.store'
import ReviewPageHeader from './partials/ReviewPageHeader.vue'
import OrderItemsPanel from './partials/OrderItemsPanel.vue'
import SamplesPanel from './partials/SamplesPanel.vue'
import ReviewProgress from './partials/ReviewProgress.vue'
import RejectSampleModal from './partials/RejectSampleModal/index.vue'

const route = useRoute()
const { fetchSamples } = useSampleStore()
const { loadOrder } = useServiceOrderStore()

onMounted(() => {
  const id = route.params.id as string
  fetchSamples(id)
  loadOrder(id)
})
</script>

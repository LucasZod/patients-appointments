<template>
  <PageHeader />
  <AppCard>
    <TubeRow v-for="tube in requiredTubes" :key="tube.type" :tube="tube" />
    <CollectionSummary />
  </AppCard>
  <ConfirmButton />
</template>

<script setup lang="ts">
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import { useSampleStore } from '@/stores/sample.store'
import AppCard from '@/shared/ui/AppCard.vue'
import PageHeader from './partials/PageHeader.vue'
import TubeRow from './partials/TubeRow.vue'
import CollectionSummary from './partials/CollectionSummary.vue'
import ConfirmButton from './partials/ConfirmButton.vue'

const route = useRoute()
const serviceOrderStore = useServiceOrderStore()
const { currentOrder } = storeToRefs(serviceOrderStore)
const { loadOrder } = serviceOrderStore
const { initTubeQuantities } = useSampleStore()

const requiredTubes = computed(() => {
  const map = new Map<string, { type: string; exams: string[] }>()
  for (const item of currentOrder.value?.items ?? []) {
    const entry = map.get(item.tubeType)
    if (entry) entry.exams.push(item.examName)
    else map.set(item.tubeType, { type: item.tubeType, exams: [item.examName] })
  }
  return [...map.values()]
})

onMounted(async () => {
  await loadOrder(route.params.id as string)
  initTubeQuantities(currentOrder.value?.items.map((i) => i.tubeType) ?? [])
})
</script>

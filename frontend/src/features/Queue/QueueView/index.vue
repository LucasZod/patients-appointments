<template>
  <div class="flex flex-col gap-6">
    <PageHeader />
    <QueueStats />
    <QueueFilters />
    <QueueBody />
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import { useQueueStore } from '@/stores/queue.store'
import PageHeader from './partials/PageHeader.vue'
import QueueStats from './partials/QueueStats.vue'
import QueueFilters from './partials/QueueFilters.vue'
import QueueBody from './partials/QueueBody.vue'

onMounted(() => useQueueStore().fetchQueue())
</script>

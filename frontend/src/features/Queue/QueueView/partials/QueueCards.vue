<template>
  <div class="flex flex-col gap-3">
    <QueueCard v-for="row in filtered" :key="row.order.id" :row="row" />
  </div>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useQueueStore } from '@/stores/queue.store'
import QueueCard from './QueueCard.vue'

const { filtered } = storeToRefs(useQueueStore())
</script>

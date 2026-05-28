<template>
  <QueueError v-if="error" />
  <QueueTable v-else-if="filtered.length > 0" />
  <QueueEmpty v-else />
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useQueueStore } from '@/stores/queue.store'
import QueueTable from './QueueTable.vue'
import QueueEmpty from './QueueEmpty.vue'
import QueueError from './QueueError.vue'

const { filtered, error } = storeToRefs(useQueueStore())
</script>

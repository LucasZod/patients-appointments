<template>
  <template v-if="filtered.length > 0">
    <QueueTable class="hidden md:block" />
    <QueueCards class="md:hidden" />
  </template>
  <QueueEmpty v-else />
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useQueueStore } from '@/stores/queue.store'
import QueueTable from './QueueTable.vue'
import QueueCards from './QueueCards.vue'
import QueueEmpty from './QueueEmpty.vue'

const { filtered } = storeToRefs(useQueueStore())
</script>

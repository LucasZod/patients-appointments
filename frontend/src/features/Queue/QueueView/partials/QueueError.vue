<template>
  <AppCard>
    <div class="flex items-center justify-between gap-3">
      <span class="text-sm text-error">{{ error }}</span>
      <AppButton variant="outlined" :loading="isLoading" @click="fetchQueue">Tentar de novo</AppButton>
    </div>
  </AppCard>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useQueueStore } from '@/stores/queue.store'
import AppCard from '@/shared/ui/AppCard.vue'
import AppButton from '@/shared/ui/AppButton.vue'

const store = useQueueStore()
const { error, isLoading } = storeToRefs(store)
const { fetchQueue } = store
</script>

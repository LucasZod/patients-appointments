<template>
  <div class="sticky bottom-4 rounded-card border border-border bg-surface p-4 shadow-md">
    <div class="mb-3 flex items-center justify-between">
      <p class="text-sm text-secondary">
        {{ reviewProgress.reviewed }} de {{ reviewProgress.total }} amostras conferidas
      </p>
      <AppButton variant="primary" :disabled="!canFinish" @click="router.push('/queue')">
        Finalizar
      </AppButton>
    </div>
    <div class="h-2 w-full overflow-hidden rounded-badge bg-border">
      <div
        class="h-full rounded-badge bg-primary transition-all"
        :style="{ width: `${reviewProgress.percentage}%` }"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import { storeToRefs } from 'pinia'
import { useSampleStore } from '@/stores/sample.store'
import AppButton from '@/shared/ui/AppButton.vue'

const router = useRouter()
const { reviewProgress, canFinish } = storeToRefs(useSampleStore())
</script>

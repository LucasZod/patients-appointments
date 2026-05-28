<template>
  <div class="mt-3 flex gap-2">
    <AppButton variant="outlined" class="flex-1 text-success border-success hover:bg-success/5" @click="approveSample(sampleId)">
      Aprovar
    </AppButton>
    <AppButton variant="outlined" class="flex-1 text-error border-error hover:bg-error/5" @click="openRejectModal(sampleId)">
      Rejeitar
    </AppButton>
  </div>
</template>

<script setup lang="ts">
import { useSampleStore } from '@/stores/sample.store'
import AppButton from '@/shared/ui/AppButton.vue'

defineProps<{ sampleId: string }>()

const { approveSample, openRejectModal } = useSampleStore()
</script>

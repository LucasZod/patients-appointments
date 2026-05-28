<template>
  <AppTextarea
    label="Descreva o motivo"
    placeholder="Descreva o motivo..."
    :model-value="rejectForm.notes"
    :error="validationErrors.notes"
    :rows="3"
    @update:model-value="setRejectNotes"
  />
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useSampleStore } from '@/stores/sample.store'
import AppTextarea from '@/shared/ui/AppTextarea.vue'

const store = useSampleStore()
const { rejectForm, validationErrors } = storeToRefs(store)
const { setRejectNotes } = store
</script>

<template>
  <fieldset class="mb-4 flex flex-col gap-2">
    <legend class="mb-2 text-sm font-medium text-text">Motivo</legend>
    <label
      v-for="option in reasons"
      :key="option.value"
      class="flex cursor-pointer items-center gap-3 rounded-button border px-3 py-2 transition-colors"
      :class="
        rejectForm.reason === option.value
          ? 'border-primary bg-primary/5'
          : 'border-border hover:bg-bg'
      "
    >
      <input
        type="radio"
        :value="option.value"
        :checked="rejectForm.reason === option.value"
        class="accent-primary"
        @change="setRejectReason(option.value)"
      />
      <span class="text-sm text-text">{{ option.label }}</span>
    </label>
  </fieldset>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useSampleStore } from '@/stores/sample.store'
import type { RejectionReasonCode } from '@/types'

const store = useSampleStore()
const { rejectForm } = storeToRefs(store)
const { setRejectReason } = store

const reasons: Array<{ value: RejectionReasonCode; label: string }> = [
  { value: 'InsufficientVolume', label: 'Volume insuficiente' },
  { value: 'WrongTube', label: 'Tubo incorreto' },
  { value: 'HemolyzedSample', label: 'Amostra hemolisada' },
  { value: 'IncorrectIdentification', label: 'Identificação incorreta' },
  { value: 'Other', label: 'Outro motivo' },
]
</script>

<template>
  <li class="rounded-button border border-border p-3">
    <div class="flex items-center justify-between gap-3">
      <div class="flex items-center gap-2">
        <TubeDot :tube-type="sample.tubeType as TubeType" />
        <p class="text-sm font-medium text-text">{{ tubeLabel }}</p>
      </div>
      <StatusChip :status="sample.status" />
    </div>

    <p v-if="sample.status === 'Approved'" class="mt-2 text-xs text-success">
      ✓ Conferido às {{ formatTime(sample.reviewedAt!) }}
    </p>

    <div v-else-if="sample.status === 'Rejected'" class="mt-2 rounded-button bg-error/5 px-2 py-1 text-xs text-error">
      ✕ {{ rejectionLabel }} <span v-if="sample.rejectionNotes"> — {{ sample.rejectionNotes }}</span>
    </div>

    <ReviewActions v-else-if="sample.status === 'Collected'" :sample-id="sample.id" />
  </li>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import TubeDot from '@/shared/ui/TubeDot.vue'
import StatusChip from '@/shared/ui/StatusChip.vue'
import ReviewActions from './ReviewActions.vue'
import type { Sample, TubeType } from '@/types'

const props = defineProps<{ sample: Sample }>()

const tubeLabels: Record<string, string> = {
  purple: 'Tubo roxo (EDTA)',
  yellow: 'Tubo amarelo (Soro)',
  green: 'Tubo verde (Heparina)',
  urine: 'Coletor de urina',
}
const tubeLabel = computed(() => tubeLabels[props.sample.tubeType] ?? props.sample.tubeType)

const rejectionLabels: Record<string, string> = {
  InsufficientVolume: 'Volume insuficiente',
  WrongTube: 'Tubo incorreto',
  HemolyzedSample: 'Amostra hemolisada',
  IncorrectIdentification: 'Identificação incorreta',
  Other: 'Outro motivo',
}
const rejectionLabel = computed(
  () => rejectionLabels[props.sample.rejectionReason ?? ''] ?? props.sample.rejectionReason,
)

const formatTime = (iso: string) =>
  new Date(iso).toLocaleTimeString('pt-BR', { hour: '2-digit', minute: '2-digit' })
</script>

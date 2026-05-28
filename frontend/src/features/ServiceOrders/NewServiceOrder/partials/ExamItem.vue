<template>
  <label
    :class="[
      'flex cursor-pointer items-center justify-between gap-3 rounded-button border px-3 py-2 transition-colors',
      checked ? 'border-primary bg-primary/5' : 'border-border bg-surface hover:bg-bg',
    ]"
  >
    <div class="flex items-center gap-3">
      <input
        type="checkbox"
        class="h-4 w-4 cursor-pointer accent-primary"
        :checked="checked"
        @change="toggleExam(exam.code)"
      />
      <TubeDot :tube-type="exam.tubeType" :size="12" />
      <span class="text-sm text-text">{{ exam.name }}</span>
      <span class="text-xs text-secondary">({{ exam.code }})</span>
    </div>
    <AppBadge variant="neutral">{{ exam.tubeLabel }}</AppBadge>
  </label>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import TubeDot from '@/shared/ui/TubeDot.vue'
import AppBadge from '@/shared/ui/AppBadge.vue'
import type { CatalogExam } from '@/data/exams-catalog'

const props = defineProps<{ exam: CatalogExam }>()

const store = useServiceOrderStore()
const { selectedExamCodes } = storeToRefs(store)
const { toggleExam } = store

const checked = computed(() => selectedExamCodes.value.includes(props.exam.code))
</script>

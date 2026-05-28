<template>
  <div class="mt-4 flex flex-col gap-1 rounded-button bg-bg px-4 py-3">
    <p class="text-sm text-secondary">
      Total de tubos: <span class="font-semibold text-text">{{ totalTubes }}</span>
    </p>
    <p class="text-sm text-secondary">
      Exames cobertos:
      <span :class="allCovered ? 'text-success font-semibold' : 'font-semibold text-text'">
        {{ examsCovered }} de {{ totalExams }}
      </span>
      <span v-if="allCovered" class="ml-1 text-success">✓</span>
    </p>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { storeToRefs } from 'pinia'
import { useSampleStore } from '@/stores/sample.store'
import { useServiceOrderStore } from '@/stores/service-order.store'

const { tubeQuantities } = storeToRefs(useSampleStore())
const { currentOrder } = storeToRefs(useServiceOrderStore())

const totalTubes = computed(() =>
  Object.values(tubeQuantities.value).reduce((sum, q) => sum + q, 0),
)

const totalExams = computed(() => currentOrder.value?.items.length ?? 0)

const examsCovered = computed(() => {
  return (
    currentOrder.value?.items.filter((item) => (tubeQuantities.value[item.tubeType] ?? 0) > 0)
      .length ?? 0
  )
})

const allCovered = computed(() => examsCovered.value === totalExams.value && totalExams.value > 0)
</script>

<template>
  <div class="flex items-center justify-between gap-4 flex-wrap">
    <div class="flex items-center gap-3">
      <AppButton variant="ghost" @click="router.back()">← Voltar</AppButton>
      <div>
        <h1 class="text-lg font-semibold text-text">{{ currentOrder!.patientName }}</h1>
        <p class="text-sm text-secondary">CPF: {{ formattedCpf }}</p>
      </div>
    </div>
    <div class="flex items-center gap-2">
      <PriorityBadge :priority="currentOrder!.priority" />
      <StatusChip :status="currentOrder!.status" />
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { storeToRefs } from 'pinia'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppButton from '@/shared/ui/AppButton.vue'
import PriorityBadge from '@/shared/ui/PriorityBadge.vue'
import StatusChip from '@/shared/ui/StatusChip.vue'

const router = useRouter()
const { currentOrder } = storeToRefs(useServiceOrderStore())

const formattedCpf = computed(() => {
  const cpf = currentOrder.value?.patientCpf
  if (!cpf || cpf.length !== 11) return cpf ?? '—'
  return `${cpf.slice(0, 3)}.${cpf.slice(3, 6)}.${cpf.slice(6, 9)}-${cpf.slice(9)}`
})
</script>

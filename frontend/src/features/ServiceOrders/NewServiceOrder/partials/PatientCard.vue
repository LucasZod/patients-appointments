<template>
  <AppCard v-if="selectedPatient">
    <div class="flex items-start gap-4 justify-between">
      <div class="flex gap-3">
        <div
          class="flex h-12 w-12 items-center justify-center rounded-full bg-primary text-sm font-semibold text-white"
        >
          {{ initials }}
        </div>
        <div class="flex flex-1 flex-col">
          <span class="text-base font-semibold text-text">{{ selectedPatient.name }}</span>
          <span class="text-sm text-secondary"
            >CPF {{ formattedCpf }} · {{ formattedBirthDate }}</span
          >
          <span v-if="selectedPatient.phone" class="text-sm text-secondary">{{
            selectedPatient.phone
          }}</span>
        </div>
      </div>
      <AppButton variant="ghost" @click="clearSearch">Trocar</AppButton>
    </div>
  </AppCard>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { storeToRefs } from 'pinia'
import { usePatientStore } from '@/stores/patient.store'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppCard from '@/shared/ui/AppCard.vue'
import AppButton from '@/shared/ui/AppButton.vue'

const { selectedPatient } = storeToRefs(useServiceOrderStore())
const { clearSearch } = usePatientStore()

const initials = computed(() => {
  const name = selectedPatient.value?.name ?? ''
  const parts = name.trim().split(/\s+/)
  const first = parts[0]?.[0] ?? ''
  const last = parts.length > 1 ? (parts[parts.length - 1]?.[0] ?? '') : ''
  return (first + last).toUpperCase()
})

const formattedCpf = computed(() => {
  const digits = selectedPatient.value?.cpf ?? ''
  return digits.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4')
})

const formattedBirthDate = computed(() => {
  const value = selectedPatient.value?.birthDate
  if (!value) return ''
  const [y, m, d] = value.split('-')
  return `${d}/${m}/${y}`
})
</script>

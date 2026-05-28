<template>
  <AppCard :padding="'lg'">
    <div class="flex flex-col gap-3">
      <h2 class="text-sm font-semibold text-secondary uppercase">Paciente</h2>
      <form class="flex items-end gap-2" @submit.prevent="searchByCpf">
        <div class="flex-1">
          <AppInput
            label="CPF"
            placeholder="000.000.000-00"
            mask="cpf"
            :model-value="searchCpf"
            :error="searchError ?? undefined"
            :disabled="isSearching"
            @update:model-value="setSearchCpf"
          />
        </div>
        <AppButton
          type="submit"
          variant="primary"
          :loading="isSearching"
          :disabled="!isSearchCpfValid"
        >
          Buscar
        </AppButton>
      </form>
      <PatientNotFound v-if="showNotFound" />
    </div>
  </AppCard>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { storeToRefs } from 'pinia'
import { usePatientStore } from '@/stores/patient.store'
import { useServiceOrderStore } from '@/stores/service-order.store'
import AppCard from '@/shared/ui/AppCard.vue'
import AppInput from '@/shared/ui/AppInput.vue'
import AppButton from '@/shared/ui/AppButton.vue'
import PatientNotFound from './PatientNotFound.vue'

const patientStore = usePatientStore()
const { searchCpf, isSearching, searchError, showRegisterModal, isSearchCpfValid } =
  storeToRefs(patientStore)
const { setSearchCpf, searchByCpf } = patientStore

const { selectedPatient } = storeToRefs(useServiceOrderStore())

const showNotFound = computed(
  () =>
    !isSearching.value &&
    !searchError.value &&
    !selectedPatient.value &&
    !showRegisterModal.value &&
    isSearchCpfValid.value,
)
</script>

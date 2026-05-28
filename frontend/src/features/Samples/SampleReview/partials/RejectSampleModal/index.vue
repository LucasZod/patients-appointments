<template>
  <AppModal :open="rejectModalOpen" title="Motivo da Rejeição" @close="closeRejectModal">
    <ReasonSelector />
    <OtherReasonField v-if="rejectForm.reason === 'Other'" />
    <ModalActions />
  </AppModal>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { useSampleStore } from '@/stores/sample.store'
import AppModal from '@/shared/ui/AppModal.vue'
import ReasonSelector from './partials/ReasonSelector.vue'
import OtherReasonField from './partials/OtherReasonField.vue'
import ModalActions from './partials/ModalActions.vue'

const store = useSampleStore()
const { rejectModalOpen, rejectForm } = storeToRefs(store)
const { closeRejectModal } = store
</script>

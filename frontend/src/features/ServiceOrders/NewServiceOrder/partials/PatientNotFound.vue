<template>
  <div
    class="flex items-center justify-between rounded-button border border-border bg-bg px-3 py-2"
  >
    <span class="text-sm text-text">Nenhum paciente encontrado com o CPF informado.</span>
    <AppButton variant="outlined" @click="openRegisterModal">Cadastrar novo paciente</AppButton>
  </div>
</template>

<script setup lang="ts">
import { usePatientStore } from '@/stores/patient.store'
import AppButton from '@/shared/ui/AppButton.vue'

const { openRegisterModal } = usePatientStore()
</script>

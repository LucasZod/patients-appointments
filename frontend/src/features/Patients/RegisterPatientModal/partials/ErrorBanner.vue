<template>
  <div v-if="registerError" class="rounded-button bg-error/10 px-3 py-2 text-sm text-error">
    {{ registerError }}
  </div>
</template>

<script setup lang="ts">
import { storeToRefs } from 'pinia'
import { usePatientStore } from '@/stores/patient.store'

const { registerError } = storeToRefs(usePatientStore())
</script>

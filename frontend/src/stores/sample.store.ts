import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { toast } from 'vue-sonner'
import router from '@/router'
import { sampleApi } from '@/api/sample.api'
import { serviceOrderApi } from '@/api/service-order.api'
import { rejectSampleSchema } from '@/schemas/sample.schema'
import { HttpError } from '@/api/http'
import type { RejectionReasonCode, Sample } from '@/types'
import type { RejectSampleInput } from '@/schemas/sample.schema'

export const useSampleStore = defineStore('sample', () => {
  const samples = ref<Sample[]>([])
  const isLoading = ref(false)
  const error = ref<string | null>(null)
  const rejectModalOpen = ref(false)
  const selectedSampleId = ref<string | null>(null)
  const isSubmitting = ref(false)
  const validationErrors = ref<Record<string, string>>({})
  const tubeQuantities = ref<Record<string, number>>({})
  const rejectForm = ref<{ reason: RejectionReasonCode; notes: string }>({
    reason: 'InsufficientVolume',
    notes: '',
  })

  const reviewProgress = computed(() => {
    const total = samples.value.length
    const reviewed = samples.value.filter(
      (s) => s.status === 'Approved' || s.status === 'Rejected',
    ).length
    const percentage = total === 0 ? 0 : Math.round((reviewed / total) * 100)
    return { reviewed, total, percentage }
  })

  const allReviewed = computed(
    () => samples.value.length > 0 && samples.value.every((s) => s.status !== 'Collected'),
  )

  const canFinish = computed(() => allReviewed.value)

  const fetchSamples = async (serviceOrderId: string): Promise<void> => {
    isLoading.value = true
    error.value = null
    try {
      samples.value = await sampleApi.findByOrder(serviceOrderId)
    } catch (err) {
      toast.error(err instanceof HttpError ? err.message : 'Erro ao carregar amostras')
    } finally {
      isLoading.value = false
    }
  }

  const approveSample = async (sampleId: string): Promise<void> => {
    try {
      const updated = await sampleApi.approve(sampleId)
      const idx = samples.value.findIndex((s) => s.id === sampleId)
      if (idx >= 0) samples.value[idx] = updated
      toast.success('Amostra aprovada!')
    } catch (err) {
      toast.error(err instanceof HttpError ? err.message : 'Erro ao aprovar amostra')
    }
  }

  const openRejectModal = (sampleId: string): void => {
    selectedSampleId.value = sampleId
    rejectModalOpen.value = true
    validationErrors.value = {}
    rejectForm.value = { reason: 'InsufficientVolume', notes: '' }
  }

  const setRejectReason = (reason: RejectionReasonCode): void => {
    rejectForm.value.reason = reason
    validationErrors.value = {}
  }

  const setRejectNotes = (notes: string): void => {
    rejectForm.value.notes = notes
  }

  const closeRejectModal = (): void => {
    rejectModalOpen.value = false
    selectedSampleId.value = null
    validationErrors.value = {}
  }

  const rejectSample = async (): Promise<void> => {
    const parsed = rejectSampleSchema.safeParse(rejectForm.value)
    if (!parsed.success) {
      const errors = parsed.error.flatten().fieldErrors
      validationErrors.value = Object.fromEntries(
        Object.entries(errors).map(([k, v]) => [k, v?.[0] ?? '']),
      )
      return
    }

    if (!selectedSampleId.value) return

    isSubmitting.value = true
    error.value = null
    try {
      const updated = await sampleApi.reject(selectedSampleId.value, parsed.data as RejectSampleInput)
      const idx = samples.value.findIndex((s) => s.id === selectedSampleId.value)
      if (idx >= 0) samples.value[idx] = updated
      closeRejectModal()
      toast.success('Amostra rejeitada.')
    } catch (err) {
      toast.error(err instanceof HttpError ? err.message : 'Erro ao rejeitar amostra')
    } finally {
      isSubmitting.value = false
    }
  }

  const initTubeQuantities = (tubeTypes: string[]): void => {
    tubeQuantities.value = Object.fromEntries([...new Set(tubeTypes)].map((t) => [t, 1]))
  }

  const setTubeQuantity = (type: string, quantity: number): void => {
    if (quantity < 1) return
    tubeQuantities.value[type] = quantity
  }

  const recordSamples = async (serviceOrderId: string, tubes: string[]): Promise<void> => {
    isSubmitting.value = true
    error.value = null
    try {
      await serviceOrderApi.completeCollection(serviceOrderId)
      await sampleApi.recordSamples(serviceOrderId, tubes)
      await router.replace(`/orders/${serviceOrderId}/samples/review`)
    } catch (err) {
      toast.error(err instanceof HttpError ? err.message : 'Erro ao registrar coleta')
    } finally {
      isSubmitting.value = false
    }
  }

  return {
    samples,
    isLoading,
    error,
    rejectModalOpen,
    selectedSampleId,
    isSubmitting,
    validationErrors,
    tubeQuantities,
    rejectForm,
    reviewProgress,
    allReviewed,
    canFinish,
    fetchSamples,
    approveSample,
    openRejectModal,
    closeRejectModal,
    setRejectReason,
    setRejectNotes,
    rejectSample,
    initTubeQuantities,
    setTubeQuantity,
    recordSamples,
  }
})

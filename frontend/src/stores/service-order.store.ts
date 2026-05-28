import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import router from '@/router'
import { serviceOrderApi } from '@/api/service-order.api'
import { examsCatalog, findExam } from '@/data/exams-catalog'
import { HttpError } from '@/api/http'
import type { Patient, Priority, ServiceOrder, TubeType } from '@/types'

export interface TubeSummaryItem {
  tubeType: TubeType
  count: number
  exams: string[]
}

export const useServiceOrderStore = defineStore('service-order', () => {
  const selectedPatient = ref<Patient | null>(null)
  const selectedPriority = ref<Priority>('Normal')
  const selectedExamCodes = ref<string[]>([])
  const isCreating = ref(false)
  const createError = ref<string | null>(null)

  const canSubmit = computed(
    () => selectedPatient.value !== null && selectedExamCodes.value.length > 0 && !isCreating.value,
  )

  const tubeSummary = computed<TubeSummaryItem[]>(() => {
    const map = new Map<TubeType, TubeSummaryItem>()
    for (const code of selectedExamCodes.value) {
      const exam = findExam(code)
      if (!exam) continue
      const current = map.get(exam.tubeType)
      if (current) {
        current.count += 1
        current.exams.push(exam.name)
      } else {
        map.set(exam.tubeType, { tubeType: exam.tubeType, count: 1, exams: [exam.name] })
      }
    }
    return [...map.values()]
  })

  const selectPatient = (patient: Patient | null) => {
    selectedPatient.value = patient
  }

  const setPriority = (priority: Priority) => {
    selectedPriority.value = priority
  }

  const toggleExam = (code: string) => {
    const idx = selectedExamCodes.value.indexOf(code)
    if (idx >= 0) selectedExamCodes.value.splice(idx, 1)
    else selectedExamCodes.value.push(code)
  }

  const reset = () => {
    selectedPriority.value = 'Normal'
    selectedExamCodes.value = []
    createError.value = null
  }

  const clearError = () => {
    createError.value = null
  }

  const createOrder = async (): Promise<ServiceOrder | null> => {
    if (!selectedPatient.value || selectedExamCodes.value.length === 0) return null

    isCreating.value = true
    createError.value = null

    try {
      const items = selectedExamCodes.value
        .map((code) => examsCatalog.find((e) => e.code === code))
        .filter((e): e is (typeof examsCatalog)[number] => e !== undefined)
        .map((e) => ({ examCode: e.code, examName: e.name, tubeType: e.tubeType }))

      const order = await serviceOrderApi.create({
        patientId: selectedPatient.value.id,
        priority: selectedPriority.value,
        items,
      })

      reset()
      await router.push('/queue')
      return order
    } catch (err) {
      createError.value = err instanceof HttpError ? err.message : 'Erro ao criar a ordem'
      return null
    } finally {
      isCreating.value = false
    }
  }

  return {
    selectedPatient,
    selectedPriority,
    selectedExamCodes,
    isCreating,
    createError,
    canSubmit,
    tubeSummary,
    selectPatient,
    setPriority,
    toggleExam,
    reset,
    clearError,
    createOrder,
  }
})

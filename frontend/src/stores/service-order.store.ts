import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { toast } from 'vue-sonner'
import router from '@/router'
import { serviceOrderApi } from '@/api/service-order.api'
import { examsCatalog, findExam } from '@/data/exams-catalog'
import { HttpError } from '@/api/http'
import type { Patient, Priority, ServiceOrder, TubeType } from '@/types'
import { usePatientStore } from './patient.store'

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

  const currentOrder = ref<ServiceOrder | null>(null)
  const isLoadingOrder = ref(false)

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
    selectedPatient.value = null
    usePatientStore().searchCpf = ''
    usePatientStore().searchError = null
  }

  const loadOrder = async (id: string): Promise<void> => {
    isLoadingOrder.value = true
    try {
      currentOrder.value = await serviceOrderApi.findById(id)
    } catch (err) {
      toast.error(err instanceof HttpError ? err.message : 'Erro ao carregar a ordem')
    } finally {
      isLoadingOrder.value = false
    }
  }

  const completeCollection = async (id: string): Promise<void> => {
    try {
      currentOrder.value = await serviceOrderApi.completeCollection(id)
    } catch (err) {
      toast.error(err instanceof HttpError ? err.message : 'Erro ao finalizar coleta')
    }
  }

  const createOrder = async (): Promise<ServiceOrder | null> => {
    if (!selectedPatient.value || selectedExamCodes.value.length === 0) return null

    isCreating.value = true

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

      toast.success('Ordem de serviço criada com sucesso!')
      await router.push('/queue')
      return order
    } catch (err) {
      toast.error(err instanceof HttpError ? err.message : 'Erro ao criar a ordem')
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
    currentOrder,
    isLoadingOrder,
    canSubmit,
    tubeSummary,
    selectPatient,
    setPriority,
    toggleExam,
    reset,
    loadOrder,
    completeCollection,
    createOrder,
  }
})

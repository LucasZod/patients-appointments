import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { cpf as cpfValidator } from 'cpf-cnpj-validator'
import { patientApi } from '@/api/patient.api'
import { HttpError } from '@/api/http'
import { registerPatientSchema } from '@/schemas/patient.schema'
import { useServiceOrderStore } from './service-order.store'

interface RegisterForm {
  name: string
  cpf: string
  birthDate: string
  phone: string
}

const emptyRegisterForm = (): RegisterForm => ({
  name: '',
  cpf: '',
  birthDate: '',
  phone: '',
})

const onlyDigits = (value: string): string => value.replace(/\D/g, '')

export const usePatientStore = defineStore('patient', () => {
  const searchCpf = ref('')
  const isSearching = ref(false)
  const searchError = ref<string | null>(null)

  const showRegisterModal = ref(false)
  const isRegistering = ref(false)
  const registerError = ref<string | null>(null)
  const registerForm = ref<RegisterForm>(emptyRegisterForm())
  const validationErrors = ref<Record<string, string>>({})

  const isSearchCpfValid = computed(() => {
    const digits = onlyDigits(searchCpf.value)
    return digits.length === 11 && cpfValidator.isValid(digits)
  })

  const clearSearch = () => {
    searchCpf.value = ''
    searchError.value = null
    useServiceOrderStore().selectPatient(null)
  }

  const searchByCpf = async () => {
    const digits = onlyDigits(searchCpf.value)
    if (!cpfValidator.isValid(digits)) {
      searchError.value = 'CPF inválido'
      return
    }

    isSearching.value = true
    searchError.value = null

    try {
      const patient = await patientApi.findByCpf(digits)
      useServiceOrderStore().selectPatient(patient)
    } catch (err) {
      useServiceOrderStore().selectPatient(null)

      if (err instanceof HttpError && err.status === 404) {
        registerForm.value = { ...emptyRegisterForm(), cpf: digits }
        showRegisterModal.value = true
        return
      }

      searchError.value = err instanceof Error ? err.message : 'Erro ao buscar paciente'
    } finally {
      isSearching.value = false
    }
  }

  const setSearchCpf = (value: string) => {
    searchCpf.value = value
    const digits = onlyDigits(value)

    useServiceOrderStore().selectPatient(null)

    if (digits.length < 11) {
      searchError.value = null
      return
    }

    if (cpfValidator.isValid(digits)) {
      searchError.value = null
      searchByCpf()
    } else {
      searchError.value = 'CPF inválido'
    }
  }

  const openRegisterModal = () => {
    registerForm.value = { ...emptyRegisterForm(), cpf: onlyDigits(searchCpf.value) }
    showRegisterModal.value = true
  }

  const closeRegisterModal = () => {
    showRegisterModal.value = false
    registerError.value = null
    validationErrors.value = {}
  }

  const setRegisterField = <K extends keyof RegisterForm>(key: K, value: RegisterForm[K]) => {
    registerForm.value[key] = value
    if (validationErrors.value[key]) {
      const { [key]: _removed, ...rest } = validationErrors.value
      validationErrors.value = rest
    }
  }

  const registerPatient = async () => {
    const payload = {
      ...registerForm.value,
      cpf: onlyDigits(registerForm.value.cpf),
      phone: registerForm.value.phone || undefined,
    }

    const parsed = registerPatientSchema.safeParse(payload)
    if (!parsed.success) {
      const fieldErrors = parsed.error.flatten().fieldErrors
      validationErrors.value = Object.fromEntries(
        Object.entries(fieldErrors).map(([k, v]) => [k, v?.[0] ?? '']),
      )
      return
    }

    isRegistering.value = true
    registerError.value = null
    validationErrors.value = {}

    try {
      const patient = await patientApi.register(parsed.data)
      useServiceOrderStore().selectPatient(patient)
      showRegisterModal.value = false
      registerForm.value = emptyRegisterForm()
    } catch (err) {
      registerError.value = err instanceof Error ? err.message : 'Erro ao cadastrar paciente'
    } finally {
      isRegistering.value = false
    }
  }

  return {
    searchCpf,
    isSearching,
    searchError,
    showRegisterModal,
    isRegistering,
    registerError,
    registerForm,
    validationErrors,
    isSearchCpfValid,
    setSearchCpf,
    clearSearch,
    searchByCpf,
    openRegisterModal,
    closeRegisterModal,
    setRegisterField,
    registerPatient,
  }
})

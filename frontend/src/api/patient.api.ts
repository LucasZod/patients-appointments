import { http } from './http'
import type { Patient } from '@/types'
import type { RegisterPatientInput } from '@/schemas/patient.schema'

export const patientApi = {
  findByCpf: (cpf: string) => http.get<Patient>(`/patients?cpf=${cpf}`),

  register: (data: RegisterPatientInput) => http.post<Patient>('/patients', data),
}

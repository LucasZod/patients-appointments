import { http } from './http'
import type { ServiceOrder } from '@/types'

export interface CreateServiceOrderBody {
  patientId: string
  priority: 'Normal' | 'Preferred' | 'Urgent'
  items: Array<{ examCode: string; examName: string; tubeType: string }>
}

export const serviceOrderApi = {
  create: (data: CreateServiceOrderBody) => http.post<ServiceOrder>('/service-orders', data),

  findById: (id: string) => http.get<ServiceOrder>(`/service-orders/${id}`),

  callNext: () => http.post<ServiceOrder>('/service-orders/call-next'),

  completeCollection: (id: string) =>
    http.patch<ServiceOrder>(`/service-orders/${id}/complete-collection`),
}

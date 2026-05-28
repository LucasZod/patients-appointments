import { http } from './http'
import type { ServiceOrder, ServiceOrderStats, ServiceOrderStatus } from '@/types'

export interface CreateServiceOrderBody {
  patientId: string
  priority: 'Normal' | 'Preferred' | 'Urgent'
  items: Array<{ examCode: string; examName: string; tubeType: string }>
}

export interface ListServiceOrdersParams {
  status?: ServiceOrderStatus
  date?: 'today'
}

const buildQuery = (params: ListServiceOrdersParams): string => {
  const search = new URLSearchParams()
  if (params.status) search.set('status', params.status)
  if (params.date) search.set('date', params.date)
  const qs = search.toString()
  return qs ? `?${qs}` : ''
}

export const serviceOrderApi = {
  create: (data: CreateServiceOrderBody) => http.post<ServiceOrder>('/service-orders', data),

  list: (params: ListServiceOrdersParams = {}) =>
    http.get<ServiceOrder[]>(`/service-orders${buildQuery(params)}`),

  stats: (date: 'today' = 'today') =>
    http.get<ServiceOrderStats>(`/service-orders/stats?date=${date}`),

  findById: (id: string) => http.get<ServiceOrder>(`/service-orders/${id}`),

  callNext: () => http.post<ServiceOrder>('/service-orders/call-next'),

  completeCollection: (id: string) =>
    http.patch<ServiceOrder>(`/service-orders/${id}/complete-collection`),
}

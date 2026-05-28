import { http } from './http'
import type { Sample } from '@/types'
import type { RejectSampleInput } from '@/schemas/sample.schema'

export const sampleApi = {
  recordSamples: (serviceOrderId: string, tubeTypes: string[]) =>
    http.post<Sample[]>('/samples', { serviceOrderId, tubeTypes }),

  findByOrder: (serviceOrderId: string) =>
    http.get<Sample[]>(`/samples?serviceOrderId=${serviceOrderId}`),

  approve: (sampleId: string) => http.patch<Sample>(`/samples/${sampleId}/approve`),

  reject: (sampleId: string, data: RejectSampleInput) =>
    http.patch<Sample>(`/samples/${sampleId}/reject`, data),
}

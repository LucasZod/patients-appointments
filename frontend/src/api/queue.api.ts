import { http } from './http'
import type { QueueItem } from '@/types'

export const queueApi = {
  getQueue: () => http.get<QueueItem[]>('/queue'),

  getPosition: (serviceOrderId: string) => http.get<QueueItem>(`/queue/${serviceOrderId}`),
}

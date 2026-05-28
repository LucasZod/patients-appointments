import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { toast } from 'vue-sonner'
import { serviceOrderApi } from '@/api/service-order.api'
import { HttpError } from '@/api/http'
import type { Priority, ServiceOrder, ServiceOrderStats, ServiceOrderStatus } from '@/types'

export type QueueFilter = 'all' | ServiceOrderStatus

export interface QueueRow {
  order: ServiceOrder
  position: number | null
  waitingMinutes: number
  tubeTypes: string[]
}

const PRIORITY_RANK: Record<Priority, number> = { Urgent: 0, Preferred: 1, Normal: 2 }

const emptyStats = (): ServiceOrderStats => ({
  waiting: 0,
  inProgress: 0,
  completedToday: 0,
  rejectedToday: 0,
})

export const useQueueStore = defineStore('queue', () => {
  const orders = ref<ServiceOrder[]>([])
  const stats = ref<ServiceOrderStats>(emptyStats())
  const isLoading = ref(false)
  const isCallingNext = ref(false)
  const error = ref<string | null>(null)
  const filterStatus = ref<QueueFilter>('all')
  const searchTerm = ref('')

  const waitingPositions = computed(() => {
    const sorted = orders.value
      .filter((o) => o.status === 'Waiting')
      .slice()
      .sort(
        (a, b) =>
          PRIORITY_RANK[a.priority] - PRIORITY_RANK[b.priority] ||
          a.createdAt.localeCompare(b.createdAt),
      )
    const map = new Map<string, number>()
    sorted.forEach((o, index) => map.set(o.id, index + 1))
    return map
  })

  const filtered = computed<QueueRow[]>(() => {
    const term = searchTerm.value.trim().toLowerCase()
    const now = Date.now()

    return orders.value
      .filter((o) => filterStatus.value === 'all' || o.status === filterStatus.value)
      .filter((o) => term === '' || (o.patientName ?? '').toLowerCase().includes(term))
      .map((o) => ({
        order: o,
        position: waitingPositions.value.get(o.id) ?? null,
        waitingMinutes: Math.max(0, Math.floor((now - new Date(o.createdAt).getTime()) / 60000)),
        tubeTypes: [...new Set(o.items.map((i) => i.tubeType))],
      }))
  })

  const fetchQueue = async () => {
    isLoading.value = true
    error.value = null

    try {
      const [list, statsData] = await Promise.all([
        serviceOrderApi.list({ date: 'today' }),
        serviceOrderApi.stats('today'),
      ])
      orders.value = list
      stats.value = statsData
    } catch (err) {
      toast.error(err instanceof HttpError ? err.message : 'Erro ao carregar a fila')
    } finally {
      isLoading.value = false
    }
  }

  const callNext = async () => {
    isCallingNext.value = true
    error.value = null

    try {
      await serviceOrderApi.callNext()
      await fetchQueue()
    } catch (err) {
      toast.error(err instanceof HttpError ? err.message : 'Erro ao chamar o próximo paciente')
    } finally {
      isCallingNext.value = false
    }
  }

  const setFilter = (status: QueueFilter) => {
    filterStatus.value = status
  }

  const setSearch = (term: string) => {
    searchTerm.value = term
  }

  return {
    orders,
    stats,
    isLoading,
    isCallingNext,
    error,
    filterStatus,
    searchTerm,
    filtered,
    fetchQueue,
    callNext,
    setFilter,
    setSearch,
  }
})

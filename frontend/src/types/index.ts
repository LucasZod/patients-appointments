export type Priority = 'Normal' | 'Preferred' | 'Urgent'
export type ServiceOrderStatus = 'Waiting' | 'InProgress' | 'Collected' | 'Completed' | 'Rejected'
export type OrderItemStatus = 'Pending' | 'Collected' | 'Approved' | 'Rejected'
export type SampleStatus = 'Collected' | 'Approved' | 'Rejected'
export type TubeType = 'purple' | 'yellow' | 'green' | 'urine'
export type RejectionReasonCode =
  | 'InsufficientVolume'
  | 'WrongTube'
  | 'HemolyzedSample'
  | 'IncorrectIdentification'
  | 'Other'

export interface Patient {
  id: string
  name: string
  cpf: string
  birthDate: string
  phone?: string | null
  createdAt: string
}

export interface OrderItem {
  id: string
  examCode: string
  examName: string
  tubeType: string
  status: OrderItemStatus
}

export interface ServiceOrder {
  id: string
  patientId: string
  status: ServiceOrderStatus
  priority: Priority
  calledAt?: string | null
  finishedAt?: string | null
  createdAt: string
  items: OrderItem[]
}

export interface QueueItem {
  serviceOrderId: string
  patientId: string
  priority: Priority
  position: number
  createdAt: string
  waitSeconds: number
}

export interface RejectionReason {
  code: RejectionReasonCode
  notes?: string | null
}

export interface Sample {
  id: string
  serviceOrderId: string
  tubeType: string
  status: SampleStatus
  rejectionReason?: RejectionReason | null
  collectedAt: string
  reviewedAt?: string | null
  createdAt: string
}

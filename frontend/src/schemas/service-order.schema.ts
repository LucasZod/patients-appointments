import { z } from 'zod'

export const createServiceOrderSchema = z.object({
  patientId: z.string().uuid('Paciente inválido'),
  priority: z.enum(['Normal', 'Preferred', 'Urgent']),
  examCodes: z.array(z.string()).min(1, 'Selecione pelo menos um exame'),
})

export type CreateServiceOrderInput = z.infer<typeof createServiceOrderSchema>

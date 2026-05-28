import { z } from 'zod'

export const registerPatientSchema = z.object({
  name: z.string().min(3, 'Nome deve ter pelo menos 3 caracteres'),
  cpf: z.string().regex(/^\d{11}$/, 'CPF inválido — somente números, 11 dígitos'),
  birthDate: z.string().min(1, 'Data de nascimento obrigatória'),
  phone: z.string().optional(),
})

export type RegisterPatientInput = z.infer<typeof registerPatientSchema>

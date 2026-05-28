import { z } from 'zod'
import { cpf } from 'cpf-cnpj-validator'

export const registerPatientSchema = z.object({
  name: z.string().min(3, 'Nome deve ter pelo menos 3 caracteres'),
  cpf: z
    .string()
    .regex(/^\d{11}$/, 'CPF deve conter 11 dígitos')
    .refine((value) => cpf.isValid(value), 'CPF inválido'),
  birthDate: z
    .string()
    .min(1, 'Data de nascimento obrigatória')
    .refine((v) => /^\d{4}-\d{2}-\d{2}$/.test(v), 'Data inválida')
    .refine((v) => !isNaN(Date.parse(v)), 'Data inválida')
    .refine((v) => new Date(v) <= new Date(), 'Data não pode ser no futuro'),
  phone: z.string().optional(),
})

export type RegisterPatientInput = z.infer<typeof registerPatientSchema>

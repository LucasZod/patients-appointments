import { z } from 'zod'

export const rejectSampleSchema = z
  .object({
    reason: z.enum([
      'InsufficientVolume',
      'WrongTube',
      'HemolyzedSample',
      'IncorrectIdentification',
      'Other',
    ]),
    notes: z.string().optional(),
  })
  .refine((data) => data.reason !== 'Other' || (data.notes && data.notes.length > 0), {
    message: 'Descreva o motivo',
    path: ['notes'],
  })

export type RejectSampleInput = z.infer<typeof rejectSampleSchema>

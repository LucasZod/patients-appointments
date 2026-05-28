import type { TubeType } from '@/types'

export interface CatalogExam {
  code: string
  name: string
  tubeType: TubeType
  tubeLabel: string
}

export const examsCatalog: readonly CatalogExam[] = [
  { code: 'HEM', name: 'Hemograma Completo', tubeType: 'purple', tubeLabel: 'EDTA' },
  { code: 'GLI', name: 'Glicose', tubeType: 'yellow', tubeLabel: 'Seco' },
  { code: 'TSH', name: 'TSH', tubeType: 'yellow', tubeLabel: 'Seco' },
  { code: 'TGO', name: 'TGO (AST)', tubeType: 'yellow', tubeLabel: 'Seco' },
  { code: 'TGP', name: 'TGP (ALT)', tubeType: 'yellow', tubeLabel: 'Seco' },
  { code: 'URI', name: 'Urina Tipo 1', tubeType: 'urine', tubeLabel: 'Frasco' },
  { code: 'PCR', name: 'Proteína C Reativa', tubeType: 'yellow', tubeLabel: 'Seco' },
  { code: 'COL', name: 'Colesterol Total', tubeType: 'yellow', tubeLabel: 'Seco' },
  { code: 'GJM', name: 'Glicemia em Jejum', tubeType: 'green', tubeLabel: 'Fluoreto' },
]

export const findExam = (code: string): CatalogExam | undefined =>
  examsCatalog.find((e) => e.code === code)

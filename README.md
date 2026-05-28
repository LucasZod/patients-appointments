# Lab System - Apoio ao Fluxo de Atendimento de Laboratório Clínico

Sistema que cobre a fase **pré-analítica** de um laboratório: da chegada do paciente até a conferência das amostras coletadas.

---

## Como rodar

**Pré-requisitos:** Docker, .NET 8 SDK, Node.js 20+

```bash
# 1. banco
docker-compose up -d

# 2. backend
cd backend
dotnet restore
dotnet ef database update
dotnet run
# API em http://localhost:5134

# 3. frontend
cd frontend
cp .env.example .env
npm install
npm run dev
# App em http://localhost:5173

# 4. testes
docker exec lab_system_postgres psql -U postgres -c "CREATE DATABASE lab_system_test;"
cd backend.Tests && dotnet test
```

---

## O que foi implementado

O foco foi entregar o fluxo operacional completo da fase pré-analítica, de ponta a ponta:

| Etapa                   | O que faz                                                                   |
| ----------------------- | --------------------------------------------------------------------------- |
| Cadastro de paciente    | Busca por CPF, cadastra se não encontrado, idempotente por design           |
| Nova ordem de serviço   | Seleção de paciente, prioridade e exames do catálogo                        |
| Fila de atendimento     | Ordenação por prioridade, FIFO, estatísticas do dia, filtros e busca        |
| Detalhe da ordem        | Stepper de status, itens e ação contextual por status                       |
| Registrar coleta        | Técnico confirma os tubos coletados com controle de quantidade por tipo     |
| Conferência de amostras | Aprovar ou rejeitar cada amostra com motivo, progresso visual e finalização |

## O que foi cortado e por quê

- **Relatórios** - a tela existe como placeholder. Agregações por período e exportação não agregam ao fluxo operacional principal dentro do tempo disponível
- **Histórico do paciente** - possível via `GET /patients/:id/orders`, mas não entrou no escopo da interface

---

## Como pesquisei o domínio

Minha esposa é dentista e trabalha num hall clínico que tem exames dentários, laboratoriais, clínicos e médicos. Pedi pra ela perguntar à gerente de lá como funcionava a parte de exames: chegada do paciente, tipos de tubo por exame, fluxo de coleta, o que acontece antes da amostra ir pra análise.

Essa conversa me deu o que precisava. Entendi que o gargalo real da operação é a fase pré-analítica, onde erros de priorização, identificação e coleta geram retrabalho e comprometem resultados. A partir daí consegui enxergar os bounded contexts do domínio:

- **Patients** - quem é o paciente, independente do atendimento
- **ServiceOrders** - o atendimento do dia, com prioridade e exames solicitados
- **Queue** - projeção da fila, sem entidades próprias, só leitura
- **Samples** - as amostras físicas e o processo de conferência

O status da ordem seguindo `Waiting, InProgress, Collected, Completed/Rejected` veio diretamente do fluxo real que ela descreveu. Assim como os tipos de tubo: EDTA roxo, soro amarelo, fluoreto verde, frasco de urina, cada um com seus exames específicos.

---

## Decisões técnicas

**Backend - C# com DDD modular.** Cada bounded context é um módulo com seu próprio domínio, use cases, repositório e controller. O domínio é C# puro, zero ASP.NET, zero EF Core dentro das entidades. As regras de negócio ficam nas entidades, nunca nos use cases ou controllers.

**Frontend - Vue com componentização por SRP.** Cada componente tem uma responsabilidade só. A vitrine de cada feature é apenas estrutura, sem CSS, sem lógica, sem store. Os sub-componentes puxam o que precisam direto da store via `storeToRefs`, sem prop drilling. Validações com Zod antes de qualquer chamada à API.

**Processo.** Comecei com um brainstorm do domínio antes de escrever uma linha de código, mapeei entidades, status, fluxos e bounded contexts. Depois escrevi um SDD separado para backend e frontend. Usei esse SDD como contexto no Claude Cowork junto com as minhas skills de Vue e C# para gerar o código com consistência nos padrões que uso no dia a dia.

---

## Como usei IA

Usei **Claude Code** durante todo o desenvolvimento, não como autocomplete, mas como colaborador ativo no ciclo de design, implementação e revisão.

O processo foi: escrevi os SDDs com o fluxo e as decisões técnicas, criei skills com os meus padrões de código (componentização Vue, DDD no backend, validação com Zod) e usei o Claude Cowork pra gerar o código dentro desse contexto. Isso garantiu consistência nos padrões sem precisar revisar cada arquivo na mão.

**Onde ajudou de verdade:**

- Scaffolding da estrutura modular do backend e configuração do EF Core
- Consistência nos componentes Vue: vitrine, storeToRefs, sem prop drilling em dezenas de arquivos
- Velocidade na implementação de partes que são corretas mas custam tempo: configuração de Tailwind v4 com os tokens do design system, setup de migrations, configuração do Zod

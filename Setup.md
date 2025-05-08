# Configuração do Ambiente e Detalhes do Projeto

## 1. Pré-requisitos

- Instale o .NET 9 SDK.
- Instale o Docker Desktop, que inclui o Docker Compose.

## 2. Estrutura do Projeto

- Crie uma solução: `dotnet new sln -n CashFlowSolution`

- Crie os projetos:

  ```bash
  dotnet new webapi -n TransactionService --output TransactionService
  dotnet new webapi -n ConsolidationService --output ConsolidationService
  ```

- Adicione à solução:

  ```bash
  dotnet sln add TransactionService/TransactionService.csproj
  dotnet sln add ConsolidationService/ConsolidationService.csproj
  ```

## 3. Banco de Dados

- PostgreSQL em um contêiner Docker, configurado no `docker-compose.yml`.

## 4. Dockerfiles

- Cada serviço tem um `Dockerfile` para containerização.

## 5. docker-compose.yml

- Define os serviços: TransactionService, ConsolidationService e db.

## 6. Execução

- Build: `docker-compose build`
- Roda: `docker-compose up`
- Acesse as APIs via Swagger:
  - TransactionService: http://localhost:5000/swagger
  - ConsolidationService: http://localhost:5001/swagger

## O que foi feito

- Arquitetura de microservices com dois serviços e um banco de dados PostgreSQL.
- Containerização com Docker e Docker Compose.
- Swagger para documentação das APIs.
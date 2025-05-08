# Configuração do Ambiente e Detalhes do Projeto

## 1. Pré-requisitos

- Instale o .NET 9 SDK.
- Instale o Docker Desktop, que inclui o Docker Compose.

## 2. Estrutura do Projeto
```
/DesafioARCH
├── TransactionService
│   ├── Dockerfile
│   ├── TransactionService.csproj
├── TransactionService.Tests
│
├── ConsolidationService
│   ├── Dockerfile
│   ├── ConsolidationService.csproj
├── ConsolidationService.Tests
│
├── docker-compose.yml
```

## 3. Banco de Dados

- PostgreSQL em um contêiner Docker, configurado no `docker-compose.yml`.

## 4. Dockerfiles

- Cada serviço tem um `Dockerfile` para containerização.

## 5. docker-compose.yml

- Define os serviços: TransactionService, ConsolidationService e db.

## 6. Execução

- Build: `docker-compose build`
- Roda: `docker-compose up -d`
- Acesse as APIs via Swagger:
  - TransactionService: http://localhost:5000/swagger
  - ConsolidationService: http://localhost:5001/swagger

## O que foi feito

- Arquitetura de microservices com dois serviços e um banco de dados PostgreSQL.
- Containerização com Docker e Docker Compose.
- Swagger para documentação das APIs.
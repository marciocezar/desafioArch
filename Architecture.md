# Arquitetura da Solução

## Visão Geral
- **TransactionService**: Gerencia transações (débito/crédito).
- **ConsolidationService**: Gera relatórios diários de saldo.
- **Banco de Dados**: PostgreSQL compartilhado.

## Escolhas Tecnológicas
- **.NET 9**: APIs RESTful.
- **PostgreSQL**: Banco relacional robusto.
- **Docker e Docker Compose**: Containerização local (para efeutar o MVP e Testes da POC).

## Requisitos Não Funcionais
- **Escalabilidade**: Serviços escaláveis independentemente.
- **Resiliência**: Falha de um serviço não afeta o outro.
- **Desempenho**: Otimização para 50 requisições por segundo.

## Diagrama de Arquitetura
[Crie um diagrama com draw.io mostrando TransactionService e ConsolidationService conectados ao PostgreSQL]

## Passos para Montar o Desafio
1. Siga o `setup.md`.
2. Implemente modelos e controladores.
3. Configure Swagger.
4. Escreva testes.
5. Hospede no GitHub.

## Execução
- `docker-compose up`
- Acesse as APIs via Swagger.
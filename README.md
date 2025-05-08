# DesafioArch
Desafio de arquitetura de soluções com o objetivo de desenvolver uma arquitetura que integre processos e sistemas com garantia a entrega e valor a organização

# Arquitetura da Solução

## Visão Geral
A solução é composta por dois microserviços independentes que gerenciam o fluxo de caixa de um comerciante:
- **TransactionService**: Responsável por gerenciar transações (débito e crédito) com operações CRUD.
- **ConsolidationService**: Gera relatórios diários de saldo, calculando o total acumulado de transações até uma data especificada.
- **Banco de Dados**: Um banco PostgreSQL compartilhado armazena todas as transações.

## Tecnologias
- **.NET 9**: Framework para desenvolvimento das APIs RESTful, escolhido por sua performance e suporte a microserviços.
- **PostgreSQL**: Banco de dados relacional robusto, ideal para transações financeiras consistentes.
- **Docker e Docker Compose**: Utilizados para containerização, garantindo consistência no ambiente local.
- **Swagger**: Ferramenta para documentação e teste das APIs.

## Requisitos Não Funcionais
- **Escalabilidade**: Cada serviço pode ser escalado horizontalmente de forma independente.
- **Resiliência**: A falha do ConsolidationService não afeta o TransactionService, desde que o banco de dados esteja disponível.
- **Desempenho**: Índices no banco de dados otimizam consultas para suportar 50 requisições por segundo no ConsolidationService.

## Diagrama de Arquitetura
O diagrama abaixo ilustra os componentes da solução e suas interações:

```mermaid
graph TD
    A[Client] -->|HTTP/REST| B[TransactionService]
    A -->|HTTP/REST| C[ConsolidationService]
    B -->|Database Connection| D[PostgreSQL]
    C -->|Database Connection| D
    B -->|Swagger UI| E[Transaction API Docs]
    C -->|Swagger UI| F[Consolidation API Docs]
```

### Legenda do Diagrama
- **Client**: Usuário ou sistema externo que interage com os serviços via endpoints HTTP/REST.
- **TransactionService**: Microserviço para operações CRUD de transações (POST, GET).
- **ConsolidationService**: Microserviço para geração de relatórios diários (GET).
- **PostgreSQL**: Banco de dados relacional que armazena todas as transações.
- **Swagger UI**: Interface para documentação e teste das APIs, acessível em `/swagger`.

## Evoluções Futuras
### Segurança
Para melhorar a segurança da solução, é necessário abordar a exposição de senhas e credenciais atualmente definidas em texto simples nos arquivos `docker-compose.yml` e `appsettings.json`. Uma evolução planejada inclui a integração com um **gerenciador de segredos**, como:
- **AWS Secrets Manager**, **Azure Key Vault**, ou **HashiCorp Vault** para armazenar credenciais sensíveis de forma segura.
- **Docker Secrets** para gerenciar senhas em ambientes conteinerizados.
Essa abordagem reduzirá os riscos de exposição de dados sensíveis, alinhando a solução com boas práticas de segurança.

### Monitoramento e Assincronicidade
Para garantir a performance e escalabilidade da solução, é planejada a implementação de um sistema de **monitoramento** utilizando ferramentas como **Prometheus** e **Grafana**. O monitoramento permitirá:
- Rastrear métricas de desempenho, como latência, taxa de requisições e uso de recursos (CPU, memória).
- Identificar gargalos no processamento, especialmente no ConsolidationService, que realiza consultas intensivas ao banco de dados.

Com base nos dados coletados, será avaliada a necessidade de introduzir uma **fila de mensagens** (como **RabbitMQ** ou **Kafka**) para processamento assíncrono. Os benefícios da assincronicidade incluem:
- **Desacoplamento**: O TransactionService pode enviar transações para uma fila, permitindo que o ConsolidationService processe os dados em segundo plano, reduzindo a carga direta no banco de dados.
- **Escalabilidade**: A fila permite balancear a carga entre múltiplas instâncias do ConsolidationService, suportando picos de tráfego.
- **Resiliência**: Em caso de falhas temporárias no ConsolidationService, as transações ficam armazenadas na fila até serem processadas.

Essa evolução será implementada após a análise de métricas de monitoramento, garantindo que a introdução da fila seja justificada pela necessidade de desempenho e volume de dados.

## Passos para Montar o Desafio
1. Siga as instruções detalhadas no `setup.md` para configurar o ambiente e o projeto.
(Todos os detalhes das necessidades do projto estão nesse arquivo)

## Execução
- Execute o comando abaixo para iniciar os containers:
  ```bash
  docker-compose up --build
  ```
- Acesse as APIs via Swagger:
  - TransactionService: http://localhost:5000/swagger
  - ConsolidationService: http://localhost:5001/swagger
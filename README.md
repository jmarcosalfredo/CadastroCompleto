# CadastroCompleto

API REST em ASP.NET Core para cadastro completo de clientes, incluindo dados pessoais, endereço e telefones, com integração com o **Asaas**.

## Stack

* **.NET 10** / ASP.NET Core Web API
* **Entity Framework Core 10** (Npgsql / PostgreSQL)
* **Mapster** para mapeamento entre entidades e DTOs
* **Swagger / OpenAPI** para documentação e testes dos endpoints
* **xUnit** + **Moq** + **FluentAssertions** para testes

## Arquitetura

O projeto segue uma separação em camadas:

```
Controller => Service => Repository => DbContext (EF Core)
```

* **Controllers**: expõem os endpoints HTTP e traduzem requisição/resposta (via Mapster).
* **Service**: concentra as regras de negócio (ex.: reconciliação do agregado Cliente ao atualizar Endereço/Telefones, integração com o Asaas na criação do cliente). Os retornos são padronizados em um `ServiceResponse<T>` (sucesso, mensagem e dados).
* **Repositories**: acesso a dados. Operações CRUD simples usam um **Generic Repository** (`IRepository<T>` / `GenericRepositoryImpl<T>`); o Cliente tem um repositório próprio (`IClienteRepository`) por precisar de `Include` para carregar Endereço e Telefones.

## Modelo de dados

* **Cliente** — dados pessoais (nome, CPF, RG, data de nascimento, estado civil, etc.) e número do cliente no Asaas (`AsaasNumber`)

  * 1:1 com **Endereco**
  * 1:N com **Telefone**

```
Cliente (1) ── (1) Endereco
    (N) Telefone
```

## Integração com Asaas

O projeto já conta com uma implementação de integração com a API do Asaas (`IAsaasService`), usada em ClienteService.

## Como executar

1. Suba o banco com Docker: `docker compose up -d`
2. Configure sua API Key do Asaas
3. Aplique as migrations: `dotnet ef database update --project CadastroCompleto`
4. Rode a API: `dotnet run --project CadastroCompleto`


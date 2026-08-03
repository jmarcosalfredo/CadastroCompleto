# CadastroCompleto

API REST em ASP.NET Core para cadastro completo de clientes, incluindo dados pessoais, endereço e telefones.

## Stack

- **.NET 10** / ASP.NET Core Web API
- **Entity Framework Core 10** (Npgsql / PostgreSQL)
- **Swagger / OpenAPI** para documentação e testes dos endpoints

## Arquitetura

O projeto segue uma separação em camadas:

Controller => Service => Repository => DbContext (EF Core)

- **Controllers**: expõem os endpoints HTTP e traduzem requisição/resposta.
- **Service**: concentra as regras de negócio (ex.: reconciliação do agregado Cliente ao atualizar Endereço/Telefones).
- **Repositories**: acesso a dados. Operações CRUD simples usam um **Generic Repository** (`IRepository<T>` / `GenericRepositoryImpl<T>`); o Cliente tem um repositório próprio (`IClienteRepository`) por precisar de `Include` para carregar Endereço e Telefones.

## Modelo de dados

- **Cliente** — dados pessoais (nome, CPF, RG, data de nascimento, estado civil, etc.)
  - 1:1 com **Endereco**
  - 1:N com **Telefone**

```
Cliente (1) ── (1) Endereco
    (N) Telefone
```

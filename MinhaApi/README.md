# MinhaApi - Gestão de Funcionários 🏢

Esta é uma Web API robusta desenvolvida em **.NET 8** como parte dos meus estudos em Sistemas de Informação na **UFLA**. O projeto foca em boas práticas de desenvolvimento, utilizando **Clean Architecture** para separação de responsabilidades e **JWT** para segurança.

## 🛠️ Tecnologias Utilizadas

* **Linguagem:** C# (.NET 8)
* **Banco de Dados:** PostgreSQL (Entity Framework Core)
* **Arquitetura:** Clean Architecture (Domain, Application, Infrastructure, API)
* **Segurança:** Autenticação JWT (JSON Web Token)
* **Documentação:** Swagger/OpenAPI
* **Padronização:** Repository Pattern & Generic Services

## 🏗️ Estrutura do Projeto

* `Domain`: Entidades e interfaces de contrato.
* `Application`: Regras de negócio e serviços (Services).
* `Infrastructure`: Implementação de repositórios e acesso a dados (DBContext).
* `API`: Controllers e configurações de autenticação.

## 🚀 Como Executar o Projeto

1. **Clonar o repositório:**
   ```bash
   git clone [https://github.com/alebortone/ApiEmployee.git](https://github.com/alebortone/ApiEmployee.git)
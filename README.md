# OmniUser

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/91b4789862c54f058500db4447b5fb6a)](https://app.codacy.com/gh/obrennomartins/omniuser/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)

## Descrição

Este projeto foi criado como parte de um desafio técnico para um processo seletivo. O objetivo é demonstrar as operações básicas com banco de dados e a integração de APIs.

## Como executar

1. Instale o SDK do .NET 7 e baixe o código-fonte:

    ``` bash
    git clone https://github.com/seu-usuario/OmniUser.git
    cd OmniUser
    ```

2. Mude a *string* de conexão do banco de dados Postgres no arquivo `src\OmniUser.API\appsettings.json`;

3. Aplique as migrações e execute o projeto com: 
 
    ``` bash
    dotnet ef database update 
    dotnet run
    ```

## Sobre o projeto

### Tecnologias e ferramentas

* Escrito usando **.NET 7**
* **Integração com API** do ViaCEP
* Persistência de dados com **Entity Framework Core** e banco de dados **Postgres**
* Análise estática de código usando **SonarQube** e **Codacy**
* Implementação no **Serviço de Aplicativo Web do Azure** via esteiras de **CI/CD**

### Arquitetura

Neste projeto, escolhi escrever um monolito e usar a **arquitetura de 3 camadas**, porque, desta forma, é possível obter um bom equilíbrio entre rapidez no desenvolvimento e uma boa manutenção do código. 

### Funcionalidades

* **Cadastro de usuários**: permite criar, ler, atualizar e desativar usuários.
* **Cadastro de endereços**: permite criar, ler, atualizar e apagar o endereço dos usuários.
* **Busca de CEP**: Integração com a API do ViaCEP para consulta e validação de endereços.

## Contribuição

Contribuições são bem-vindas! Fique à vontade para abrir uma *issue* ou enviar um *pull request*.

## Licença

Este projeto está licenciado sob a Licença MIT.

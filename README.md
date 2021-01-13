Projeto Guideline
=====================
A proposta deste projeto/Solution é definir uma diretriz para projetos .net Core API.

Definido com uma arquitetura em Camadas:

*API
*Application
*Domain (DDD)
*Infra
 -Data
 -CrossCutting

## Como usar:
- Você precisará do Visual Studio 2019 e do .NET Core SDK mais recentes.
- ***Please check if you have installed the same runtime version (SDK) described in global.json***
- O SDK e as ferramentas mais recentes podem ser baixados em https://dot.net/core.

Além disso, você pode executar o Projeto no Visual Studio Code (Windows, Linux ou MacOS).
Para saber mais sobre como configurar seu ambiente, visite o [Guia de download do Microsoft .NET] (https://www.microsoft.com/net/download)

## Technologies implemented:

- ASP.NET Core 3.1 (with .NET Core 3.1)
 - ASP.NET WebApi Core with JWT Bearer Authentication
- Dapper
- .NET Core Native DI
- AutoMapper
- FluentValidator
- MediatR
- Swagger UI with JWT support
- .NET DevPack
- .NET DevPack.Identity

## Architecture:

- Full architecture with responsibility separation concerns, SOLID and Clean Code
- Domain Driven Design (Layers and Domain Model Pattern)
- Domain Events
- Domain Notification
- Domain Validations
- CQRS (Imediate Consistency)
- Event Sourcing
- Unit of Work
- Repository
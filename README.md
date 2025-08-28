# 📘 Guideline de Projeto Visual Studio | Clean Code | DDD

⚠️ **Status:** Este projeto está defasado, mas ainda serve como referência para diretrizes de arquitetura em APIs .NET Core.

---

## 🎯 Propósito
Definir uma diretriz para projetos **.NET Core API** utilizando:
- Arquitetura em camadas  
- Princípios de **Clean Code**  
- **Domain Driven Design (DDD)**  

---

## 🏗️ Arquitetura em Camadas

- 📡 **API**  
- 🧠 **Application**  
- 🧬 **Domain (DDD)**  
- 🛠️ **Infra**  
- 💾 **Data**  
- 🧰 **CrossCutting**  

---

## 🚀 Como Usar

1. Instale o **Visual Studio 2019** e o **.NET Core SDK** mais recente.  
2. Verifique se o SDK instalado corresponde à versão descrita no arquivo **`global.json`**.  
3. Baixe o SDK e ferramentas em 👉 [https://dot.net/core](https://dot.net/core).  
4. Também é possível executar no **Visual Studio Code** (Windows, Linux ou macOS).  

📖 Consulte o **[Guia de Download do Microsoft .NET](https://dot.net/core)** para configurar seu ambiente.

---

## 🛠️ Tecnologias Implementadas

- 🌐 **ASP.NET Core 3.1**  
- 🔐 **ASP.NET WebApi Core** com autenticação **JWT Bearer**  
- ⚡ **Dapper**  
- 🧩 **.NET Core Native DI**  
- 🔄 **AutoMapper**  
- ✅ **FluentValidator**  
- 📣 **MediatR**  
- 🧪 **Swagger UI** com suporte a JWT  
- 📦 **.NET DevPack**  
- 🔐 **.NET DevPack.Identity**  

---

## 🧱 Arquitetura e Padrões

- 🧼 Separação de responsabilidades (**SOLID + Clean Code**)  
- 🧠 **Domain Driven Design (DDD)** — camadas e padrão **Domain Model**  
- 📢 **Eventos de Domínio**  
- 🚨 **Notificações de Domínio**  
- 🧪 **Validações de Domínio**  
- ⚙️ **CQRS (consistência imediata)**  
- 🕒 **Event Sourcing**  
- 🔄 **Unit of Work**  
- 📁 **Repository Pattern**  

---

📌 Este guideline serve como **base de referência** para estruturar projetos .NET Core seguindo boas práticas de **arquitetura limpa** e **DDD**.

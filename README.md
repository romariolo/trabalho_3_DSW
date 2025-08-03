# Projeto de Biblioteca - Web Service e Containers

Este projeto é a implementação de um web service em C# para gerenciar os dados de uma aplicação de biblioteca (SPA em React), tudo containerizado com Docker. Este trabalho prático foi desenvolvido para a disciplina de Desenvolvimento de Software Web.

## 🚀 Tecnologias Utilizadas

- **Back-end:** C# 8, .NET 8, ASP.NET Core Web API
- **Banco de Dados:** Entity Framework Core (In-Memory)
- **Front-end:** React, React Router, Material-UI
- **Containerização:** Docker

## Como Executar o Projeto

### Pré-requisitos
- Docker Desktop instalado e em execução.

### Passos
1. Clone este repositório.
2. Abra um terminal na pasta raiz do projeto.
3. Construa a imagem Docker com o comando:
   ```bash
   docker build -t biblioteca-app .
   ```
4. Execute o container a partir da imagem:
   ```bash
   docker run -p 8080:8080 --rm --name minha-biblioteca biblioteca-app
   ```
5. Abra o navegador e acesse `http://localhost:8080`.

## 📸 (Screenshots)

Aqui estão as telas da aplicação funcionando, servida a partir do container Docker.

**1. Tela Inicial da Aplicação:**
*A aplicação React sendo carregada com sucesso a partir do servidor C#.*
![td3](https://github.com/user-attachments/assets/f78a7f0d-db00-49f6-86ea-4f232f79d540)



**2. Tela de Listagem de Usuários:**
*Demonstração da comunicação entre o front-end e a

 API, buscando e exibindo os dados dos usuários cadastrados no banco de dados em memória.*
 ![td3 2](https://github.com/user-attachments/assets/834e7059-ee7b-46c1-9c69-030dc4b1953e)


**3. Container Docker em execução (Exemplo):**
![td3 3](https://github.com/user-attachments/assets/ce533077-4caf-42b5-b7ee-a3a2a6d7861a)

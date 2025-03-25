# LibFlow

LibFlow é uma API REST para gerenciamento de livros e autores. A API permite listar, cadastrar, atualizar e excluir livros e autores, além de uma funcionalidade de reserva de livros.

## 📌 Funcionalidades

- Gerenciamento de autores (CRUD)
- Gerenciamento de livros (CRUD)
- Reserva de livros (a ser implementado)
  - Usuário informa nome do livro e e-mail
  - API publica mensagem em uma fila de mensagens
  - Serviço consome a mensagem e envia um e-mail de confirmação

## 🚀 Tecnologias Utilizadas

- C# / .NET
- Entity Framework Core
- PostgreSQL
- RabbitMQ (para filas de mensagens, em breve)
- SMTP/SendGrid (para envio de e-mails, em breve)

## 📦 Estrutura do Projeto

```
📦 LibFlow
 ┣ 📂 Controllers
 ┣ 📂 Models
 ┣ 📂 Services
 ┣ 📂 Data
 ┗ 📂 Dto
```

## ⚡ Instalação e Configuração

1. Clone este repositório:
   ```sh
   git clone https://github.com/seu-usuario/LibFlow.git
   ```
2. Navegue até a pasta do projeto:
   ```sh
   cd LibFlow
   ```
3. Instale as dependências:
   ```sh
   dotnet restore
   ```
4. Configure a conexão com o banco de dados no `appsettings.json`
5. Execute as migrações do banco de dados:
   ```sh
   dotnet ef database update
   ```
6. Inicie a API:
   ```sh
   dotnet run
   ```

## 🛠 Como Contribuir

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature-nova`)
3. Commit suas alterações (`git commit -m 'Adiciona nova funcionalidade'`)
4. Envie para o repositório (`git push origin feature-nova`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT.


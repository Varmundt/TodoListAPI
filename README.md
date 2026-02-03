# Todo API

API RESTful para gerenciamento de tarefas com autenticação JWT.

Há um repositório FrontEnd para testar a API, Acesse o link abaixo para acessar ele:
https://github.com/Varmundt/Todo-List-FrontEnd

## 🚀 Tecnologias

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- BCrypt para hash de senhas

## ⚙️ Configuração

1. Clone o repositório
```bash
git clone https://github.com/Varmundt/TodoListAPI.git
cd todo-api
```

2. Copie o arquivo de configuração de exemplo
```bash
cp appsettings.Example.json appsettings.json
```

3. Edite `appsettings.json` com suas credenciais:
    - Connection String do SQL Server
    - Chave JWT (mínimo 64 caracteres)

4. Execute as migrations
```bash
dotnet ef database update
```

5. Execute a aplicação
```bash
dotnet run
```

A API estará disponível em `https://localhost:5287`

## 📝 Endpoints

### Auth
- POST `/api/Auth/register` - Registrar usuário
- POST `/api/Auth/login` - Login

### Todo (requer autenticação)
- GET `/api/Todo` - Listar tarefas
- GET `/api/Todo/{id}` - Buscar tarefa
- POST `/api/Todo` - Criar tarefa
- PUT `/api/Todo/{id}` - Atualizar tarefa
- DELETE `/api/Todo/{id}` - Deletar tarefa

## 🔐 Autenticação

Use o token JWT retornado no login:
```
Authorization: Bearer {seu-token}
```

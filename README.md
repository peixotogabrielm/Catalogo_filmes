# 🎬 API Catálogo de Filmes

Uma API RESTful desenvolvida em .NET 8 para gerenciamento de um catálogo de filmes com sistema de autenticação e autorização baseado em JWT.

## ✨ Funcionalidades

### 🔐 Autenticação e Autorização
- **Login/Registro** de usuários
- **Autenticação JWT** para proteção de endpoints
- **Sistema de Roles** (User/Admin)
- **Registro de administradores** com chave secreta

### 🎭 Gerenciamento de Filmes
- **CRUD completo** de filmes (Create, Read, Update, Delete)
- **Filtros de busca** por título, gênero e ano
- **Paginação** de resultados
- **Validação** de dados de entrada

### 👥 Painel Administrativo
- **Dashboard** com estatísticas do sistema
- **Gerenciamento de usuários** (listar, atualizar roles, deletar)
- **Estatísticas de filmes** por gênero e ano
- **Controle de acesso** restrito a administradores

## 🛠️ Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **ASP.NET Core Web API** - Para criação da API REST
- **Entity Framework Core** - ORM para acesso a dados
- **SQL Server** - Banco de dados
- **JWT (JSON Web Tokens)** - Autenticação e autorização
- **BCrypt** - Hash de senhas
- **Swagger/OpenAPI** - Documentação da API
- **AutoMapper** - Mapeamento de objetos (implícito nos DTOs)

## 📋 Pré-requisitos

- .NET 8 SDK
- SQL Server (Local ou Azure)
- Visual Studio 2022 ou VS Code

## ⚙️ Configuração e Instalação

### 1. Clone o repositório
```bash
git clone https://github.com/peixotogabrielm/Catalogo_filmes.git
cd Catalogo_filmes
```

### 2. Configure o banco de dados
Edite o arquivo `appsettings.json` ou `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CatalogoFilmesDB;Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "Jwt": {
    "Secret": "sua-chave-secreta-super-segura-aqui",
    "Issuer": "CatalogoFilmesAPI",
    "Audience": "CatalogoFilmesUsers"
  }
}
```

### 3. Execute as migrações
```bash
cd CatalogoFilmes
dotnet ef database update
```

### 4. Execute a aplicação
```bash
dotnet run
```

A API estará disponível em `https://localhost:7224`

## 📚 Documentação da API

### Base URL
```
https://localhost:7224/api
```

### 🔓 Endpoints Públicos

#### Autenticação

**POST** `/auth/login`
```json
{
  "email": "usuario@email.com",
  "senha": "senha123"
}
```

**POST** `/auth/registrar`
```json
{
  "nome": "Nome do Usuário",
  "email": "usuario@email.com",
  "senha": "senha123"
}
```

**POST** `/admin/RegistrarAdmin?chaveSecreta=lM3ULXRHup`
```json
{
  "nome": "Admin",
  "email": "admin@email.com",
  "senha": "senha123"
}
```

### 🔒 Endpoints Protegidos (Requer JWT)

#### Filmes

**GET** `/filmes/GetAllFilmes`
- Query Parameters: `titulo`, `genero`, `ano`, `pageNumber`, `pageSize`

**GET** `/filmes/GetFilmeById/{id}`

**POST** `/filmes/AddFilme`
```json
{
  "titulo": "Nome do Filme",
  "genero": "Ação",
  "ano": 2024,
  "sinopse": "Descrição do filme..."
}
```

**PUT** `/filmes/UpdateFilme`
```json
{
  "id": "guid-do-filme",
  "titulo": "Nome Atualizado",
  "genero": "Drama",
  "ano": 2024,
  "sinopse": "Nova descrição..."
}
```

**DELETE** `/filmes/DeleteFilme/{id}`

### 👑 Endpoints Administrativos (Requer Role Admin)

#### Dashboard
**GET** `/admin/Dashboard` - Estatísticas gerais do sistema

#### Gerenciamento de Usuários
**GET** `/admin/Usuarios?pageNumber=1&pageSize=20` - Lista usuários com paginação

**PUT** `/admin/Usuarios/role` - Atualiza role do usuário
```json
{
  "usuarioId": "guid-do-usuario",
  "novaRole": "Admin"
}
```

**DELETE** `/admin/Usuarios/{id}` - Remove usuário

#### Estatísticas de Filmes
**GET** `/admin/Filmes/stats` - Estatísticas de filmes por gênero e ano

## 🔑 Autenticação

### Como usar JWT

1. **Faça login** para obter o token:
```bash
curl -X POST "https://localhost:7224/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email":"seu@email.com","senha":"suasenha"}'
```

2. **Use o token** em requisições protegidas:
```bash
curl -X GET "https://localhost:7224/api/filmes/GetAllFilmes" \
  -H "Authorization: Bearer SEU_TOKEN_JWT_AQUI"
```

### Swagger UI
Acesse `https://localhost:7224/swagger` para testar a API interativamente.

## 📊 Modelo de Dados

### Filme
```csharp
public class Filme
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }        // Obrigatório
    public string Genero { get; set; }        // Obrigatório
    public int Ano { get; set; }              // Obrigatório
    public string Sinopse { get; set; }
}
```

### Usuario
```csharp
public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; set; }          // Obrigatório
    public string Email { get; set; }         // Obrigatório
    public string SenhaHash { get; set; }     // Obrigatório
    public string Role { get; set; }          // "User" ou "Admin"
    public DateTime DataCriacao { get; set; }
}
```

## 🧪 Exemplos de Uso

### Registrar e fazer login
```javascript
// 1. Registrar usuário
const registro = await fetch('/api/auth/registrar', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    nome: 'João Silva',
    email: 'joao@email.com',
    senha: 'senha123'
  })
});

// 2. Fazer login
const login = await fetch('/api/auth/login', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    email: 'joao@email.com',
    senha: 'senha123'
  })
});

const { token } = await login.json();
```

### Adicionar um filme
```javascript
const novoFilme = await fetch('/api/filmes/AddFilme', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}`
  },
  body: JSON.stringify({
    titulo: 'Inception',
    genero: 'Ficção Científica',
    ano: 2010,
    sinopse: 'Dom Cobb é um ladrão especializado...'
  })
});
```

### Buscar filmes com filtros
```javascript
const filmes = await fetch('/api/filmes/GetAllFilmes?genero=Ação&ano=2023&pageSize=10', {
  headers: {
    'Authorization': `Bearer ${token}`
  }
});
```

## 🔧 Estrutura do Projeto

```
CatalogoFilmes/
├── Controllers/          # Controladores da API
│   ├── AuthController.cs
│   ├── FilmesController.cs
│   └── AdminController.cs
├── Models/              # Modelos de dados
│   ├── Filme.cs
│   └── Usuario.cs
├── DTOs/               # Data Transfer Objects
├── Services/           # Lógica de negócio
├── Repositories/       # Acesso a dados
├── Data/              # Contexto do Entity Framework
├── Helpers/           # Classes auxiliares (JWT, etc.)
└── Migrations/        # Migrações do banco de dados
```

## 🛡️ Segurança

- **Senhas** são hasheadas com BCrypt
- **JWT** com tempo de expiração configurável
- **Validação** de entrada em todos os endpoints
- **Autorização** baseada em roles
- **CORS** configurado para desenvolvimento

## 🚀 Deploy

### Usando Docker (Opcional)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CatalogoFilmes.csproj", "./"]
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogoFilmes.dll"]
```


## 👨‍💻 Autor

**Gabriel Peixoto**
- GitHub: [@peixotogabrielm](https://github.com/peixotogabrielm)

## 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.

1. Faça um Fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

---

⭐ Se este projeto foi útil para você, considere dar uma estrela!

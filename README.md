# House Finances

<details open>
<summary>🇺🇸 English</summary>

Personal household finance tracker — track transactions, categories, and people, with a dashboard showing totals and charts.

**Stack:** .NET 8 · ASP.NET Core · EF Core · PostgreSQL · React 18 · TypeScript · Vite · Tailwind CSS · Docker

---

## Prerequisites

| Tool | Minimum version |
|------|----------------|
| [Docker](https://docs.docker.com/get-docker/) | 24+ |
| [Docker Compose](https://docs.docker.com/compose/) | v2 (bundled with Docker Desktop) |

That is all you need. The database, backend, and frontend all run inside containers.

---

## Run with Docker (recommended)

```bash
# 1. Clone
git clone <repo-url>
cd house_finances

# 2. Start everything (builds images on first run)
./dev.sh
```

> On Windows without WSL: `docker compose up --build`

The app will be available at **http://localhost**.

The API (with Swagger UI) is available at **http://localhost/api** (proxied through nginx) or directly on port `5000` inside the Docker network.

On first boot, EF Core migrations run automatically and seed the database.

### Stop

```bash
docker compose down          # stop containers, keep DB volume
docker compose down -v       # stop containers AND delete DB volume (full reset)
```

---

## Run locally (without Docker)

### Backend

**Requirements:** [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0), a reachable PostgreSQL instance.

```bash
cd backend

# Point to your local PostgreSQL instance
export ConnectionStrings__DefaultConnection="Host=localhost;Database=house_finances;Username=<user>;Password=<password>"

dotnet run
# API listening on http://localhost:5000
# Swagger UI: http://localhost:5000/swagger
```

### Frontend

**Requirements:** [Node.js 20+](https://nodejs.org/)

```bash
cd frontend
npm install
npm run dev
# Dev server: http://localhost:5173
```

The Vite dev server proxies `/api` requests to the backend automatically (see [vite.config.ts](frontend/vite.config.ts)).

---

## Configuration

| Variable | Default (Docker) | Description |
|----------|-----------------|-------------|
| `ConnectionStrings__DefaultConnection` | `Host=db;Database=house_finances;Username=hfuser;Password=hfpassword` | PostgreSQL connection string |
| `ASPNETCORE_ENVIRONMENT` | `Production` | ASP.NET Core environment |
| `ASPNETCORE_URLS` | `http://+:5000` | Backend listening address |

> **Security note:** The default credentials in `docker-compose.yml` are for local development only. For any shared or production deployment, override them via environment variables or a `docker-compose.override.yml` (which is git-ignored).

---

## Project structure

```
house_finances/
├── apps/
│   ├── backend/                     # .NET 8 Backend based on Clean Architecture & DDD
│   │   ├── HouseFinances.Api/       # ASP.NET Core Web API (Presentation Layer)
│   │   ├── HouseFinances.Application/ # Use cases, DTOs, and Interfaces (Application Layer)
│   │   ├── HouseFinances.Domain/    # Entities, Enums, and Exceptions (Domain Layer)
│   │   └── HouseFinances.Infrastructure/ # EF Core, db implementations (Infrastructure Layer)
│   └── frontend/                    # React + Vite + TypeScript + Tailwind
│       ├── src/
│       └── Dockerfile
├── infra/
│   └── docker/
│       └── docker-compose.yml       # Orchestrates db · backend · frontend
├── dev.sh                           # Convenience wrapper
└── house_finances.sln
```

</details>

<details>
<summary>🇧🇷 Português</summary>

Rastreador de finanças domésticas pessoais — controle transações, categorias e pessoas, com um painel mostrando totais e gráficos.

**Stack:** .NET 8 · ASP.NET Core · EF Core · PostgreSQL · React 18 · TypeScript · Vite · Tailwind CSS · Docker

---

## Pré-requisitos

| Ferramenta | Versão mínima |
|------|----------------|
| [Docker](https://docs.docker.com/get-docker/) | 24+ |
| [Docker Compose](https://docs.docker.com/compose/) | v2 (incluído no Docker Desktop) |

Isso é tudo que você precisa. O banco de dados, o backend e o frontend executam todos dentro de contêineres.

---

## Executar com Docker (recomendado)

```bash
# 1. Clonar
git clone <repo-url>
cd house_finances

# 2. Iniciar tudo (constrói as imagens na primeira execução)
./dev.sh
```

> No Windows sem WSL: `docker compose up --build`

O aplicativo estará disponível em **http://localhost**.

A API (com Swagger UI) estará disponível em **http://localhost/api** (através do proxy do nginx) ou diretamente na porta `5000` dentro da rede Docker.

No primeiro início, as migrações do EF Core rodam automaticamente e populam o banco de dados.

### Parar

```bash
docker compose down          # para os contêineres, mantém o volume do DB
docker compose down -v       # para os contêineres E deleta o volume do DB (reset completo)
```

---

## Executar localmente (sem Docker)

### Backend

**Requisitos:** [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0), uma instância do PostgreSQL alcançável.

```bash
cd backend

# Aponte para a sua instância local do PostgreSQL
export ConnectionStrings__DefaultConnection="Host=localhost;Database=house_finances;Username=<user>;Password=<password>"

dotnet run
# API escutando em http://localhost:5000
# Swagger UI: http://localhost:5000/swagger
```

### Frontend

**Requisitos:** [Node.js 20+](https://nodejs.org/)

```bash
cd frontend
npm install
npm run dev
# Servidor dev: http://localhost:5173
```

O servidor dev do Vite redireciona automaticamente as requisições `/api` para o backend (veja [vite.config.ts](frontend/vite.config.ts)).

---

## Configuração

| Variável | Padrão (Docker) | Descrição |
|----------|-----------------|-------------|
| `ConnectionStrings__DefaultConnection` | `Host=db;Database=house_finances;Username=hfuser;Password=hfpassword` | String de conexão do PostgreSQL |
| `ASPNETCORE_ENVIRONMENT` | `Production` | Ambiente do ASP.NET Core |
| `ASPNETCORE_URLS` | `http://+:5000` | Endereço de escuta do Backend |

> **Nota de segurança:** As credenciais padrão em `docker-compose.yml` são apenas para desenvolvimento local. Para qualquer implantação compartilhada ou de produção, sobrescreva-as via variáveis de ambiente ou com um arquivo `docker-compose.override.yml` (que é ignorado pelo git).

---

## Estrutura do projeto

```text
house_finances/
├── apps/
│   ├── backend/                     # Backend .NET 8 baseado em Clean Architecture e DDD
│   │   ├── HouseFinances.Api/       # ASP.NET Core Web API (Camada de Apresentação)
│   │   ├── HouseFinances.Application/ # Casos de uso, DTOs e Interfaces (Camada de Aplicação)
│   │   ├── HouseFinances.Domain/    # Entidades, Enums e Exceções (Camada de Domínio)
│   │   └── HouseFinances.Infrastructure/ # EF Core, banco de dados (Camada de Infraestrutura)
│   └── frontend/                    # React + Vite + TypeScript + Tailwind
│       ├── src/
│       └── Dockerfile
├── infra/
│   └── docker/
│       └── docker-compose.yml       # Orquestra db · backend · frontend
├── dev.sh                           # Wrapper de conveniência
└── house_finances.sln
```
</details>

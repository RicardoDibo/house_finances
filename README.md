# House Finances

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
├── backend/               # ASP.NET Core Web API (.NET 8)
│   ├── Controllers/       # REST endpoints
│   ├── Data/              # EF Core DbContext
│   ├── DTOs/              # Request / response models
│   ├── Migrations/        # EF Core migrations
│   ├── Models/            # Domain entities
│   └── Dockerfile
├── frontend/              # React + Vite + TypeScript + Tailwind
│   ├── src/
│   └── Dockerfile
├── docker-compose.yml     # Orchestrates db · backend · frontend
├── dev.sh                 # Convenience wrapper: docker compose up --build
└── house_finances.sln
```

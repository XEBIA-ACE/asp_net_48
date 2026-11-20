# Account Management Service (scaffold)

Scaffold for story **SCAFFOLD-SVC-001** (tasks T001–T005, acceptance criterion AC-1).
It covers the service skeleton, environment-based configuration, a health-check endpoint and a
database connection helper. Authentication, OAuth 2.0 and JWT features are out of scope here.

## Stack

- .NET 8 / ASP.NET Core (REST/JSON over HTTP)
- EF Core with the Npgsql (PostgreSQL) provider
- StackExchange.Redis (caching client)
- Elastic.Clients.Elasticsearch (search client)
- Swashbuckle (OpenAPI/Swagger)

This service sits next to the existing .NET Framework 4.8 Web API in the repo root. It has its
own solution and does not change the existing application.

## Layout (hexagonal)

| Project | Role |
| --- | --- |
| `src/AccountManagement.Application` | Ports (for example `IDatabaseConnection`). No infrastructure dependencies. |
| `src/AccountManagement.Infrastructure` | Adapters: EF Core `AccountManagementDbContext`, `EfCoreDatabaseConnection`, Redis and Elasticsearch clients, `ServiceSettings`. |
| `src/AccountManagement.Api` | HTTP entrypoint (`Program.cs`), `GET /health`, Swagger. |
| `tests/AccountManagement.Tests` | xUnit tests, including the AC-1 health-check test. |

## Configuration

Configuration comes **only** from environment variables. Values are format-checked (port range, Npgsql/Redis connection-string syntax, URL). The service fails fast at startup and
names every missing or invalid variable. It never prints their values.

| Variable | Description |
| --- | --- |
| `HTTP_PORT` | Port the HTTP server listens on (1–65535). |
| `DATABASE_CONNECTION_STRING` | PostgreSQL connection string (Npgsql format). |
| `REDIS_CONNECTION_STRING` | StackExchange.Redis configuration string. |
| `ELASTICSEARCH_URL` | Absolute `http`/`https` URL of the Elasticsearch/OpenSearch node. |

The database, Redis and Elasticsearch clients connect lazily, so the service and `/health` run
even when those dependencies are down. Swagger UI (`/swagger`) is enabled only when
`ASPNETCORE_ENVIRONMENT=Development`.

## Run

```bash
cd AccountManagementService
export HTTP_PORT=... DATABASE_CONNECTION_STRING=... REDIS_CONNECTION_STRING=... ELASTICSEARCH_URL=...
dotnet run --project src/AccountManagement.Api
curl http://localhost:$HTTP_PORT/health   # {"status":"ok"}
```

## Checks

```bash
dotnet build                        # analyzers + code style enforced, warnings are errors
dotnet format --verify-no-changes   # formatting (.editorconfig)
dotnet test
```

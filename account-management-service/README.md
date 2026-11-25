# Account Management Service (scaffold)

Scaffold for story **SCAFFOLD-SVC-001** (acceptance criterion **AC-1**). This directory is a standalone
Node.js service and is independent of the existing .NET Framework 4.8 application at the repository root.

## Stack choice

The spec allows Java/Spring Boot, .NET 8, or Node.js; this scaffold uses the stack named in the AC-1
obligation for this service:

- Node.js (>= 20) + Express.js
- PostgreSQL via TypeORM (connection only; no entities, schemas, or migrations)
- ESLint + Prettier + EditorConfig
- Node built-in test runner (`node:test`) + supertest

## Layout (Hexagonal / Clean Architecture)

```
src/
  main.js                               # entrypoint (T001)
  config/index.js                       # environment-based config loading (T002)
  domain/                               # domain model (empty; not specified yet)
  application/                          # use cases / ports (empty; not specified yet)
  adapters/inbound/http/                # Express app + GET /health (T003)
  adapters/outbound/persistence/        # PostgreSQL connection helper (T004)
test/                                   # tests for AC-1
```

## Configuration

All configuration comes from environment variables. A local `.env` file (gitignored) is loaded if present;
see `.env.example`.

| Variable      | Required | Description                    |
| ------------- | -------- | ------------------------------ |
| `PORT`        | yes      | HTTP listen port               |
| `DB_HOST`     | yes      | PostgreSQL host                |
| `DB_PORT`     | yes      | PostgreSQL port                |
| `DB_NAME`     | yes      | PostgreSQL database name       |
| `DB_USER`     | yes      | PostgreSQL username            |
| `DB_PASSWORD` | yes      | PostgreSQL password            |
| `DB_SSL`      | no       | `true` to enable TLS to the DB |

The service exits with an error if a required variable is missing or the database is unreachable.

## Run

```bash
npm install
cp .env.example .env   # fill in values
npm start
curl http://localhost:$PORT/health   # {"status":"ok"}
```

## Checks

```bash
npm run lint
npm run format:check
npm test   # DB connectivity test runs only when DB_* variables are set
```

## Known gaps (SCAFFOLD-SVC-001)

- The C3/C4 diagrams and API spec for the Account Management Service are not in this repository or the
  spec export, so the layout follows the general Hexagonal/Clean Architecture constraint only.
- Account management APIs, OAuth 2.0/JWT flows, and the data schema are not specified and are not
  implemented here.
- OpenAPI/Swagger, OpenTelemetry, Redis, and OAuth2/OIDC libraries from the tech stack are not added yet;
  they are outside the scaffolding tasks for this story.

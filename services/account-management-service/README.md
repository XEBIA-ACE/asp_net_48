# Account Management Service — scaffold (SCAFFOLD-SVC-001)

Scaffold only: project skeleton, environment-based configuration, health check and a PostgreSQL
connection helper. No account, OAuth 2.0 or JWT endpoints/flows, schemas or migrations are
implemented — they are not specified in this story.

## Stack choice

Node.js (>= 20) + Express, PostgreSQL via TypeORM (`pg` driver), ESLint + Prettier,
`node:test` + supertest. Chosen because AC-1 names Node.js, Express.js and PostgreSQL.

## Layout (Hexagonal / Clean Architecture)

```
src/
  main.js                                  # entrypoint (T001)
  config/index.js                          # env config loader (T002)
  domain/                                  # domain model (empty in this story)
  application/ports/database-connection.js # outbound port
  adapters/inbound/http/                   # Express app + GET /health (T003)
  adapters/outbound/persistence/database.js# PostgreSQL helper (T004)
test/                                      # unit tests + AC-1 integration test (T005)
```

## Configuration (T002)

All values come from environment variables; see `.env.example`. No defaults, secrets or
endpoints are hardcoded. Startup fails fast if any variable is missing or invalid.

| Variable      | Purpose           |
| ------------- | ----------------- |
| `PORT`        | HTTP listen port  |
| `DB_HOST`     | PostgreSQL host   |
| `DB_PORT`     | PostgreSQL port   |
| `DB_NAME`     | Database name     |
| `DB_USER`     | Database user     |
| `DB_PASSWORD` | Database password |

## Run locally

```sh
npm install
export PORT=3000 DB_HOST=... DB_PORT=5432 DB_NAME=... DB_USER=... DB_PASSWORD=...
npm start
curl http://localhost:3000/health   # {"status":"ok"}
```

## Checks

```sh
npm run lint
npm run format:check
npm test            # AC-1 integration test runs only when DB_* variables point at a PostgreSQL
```

## Gaps (disclosed, not implemented)

- C3/C4 diagrams and the API spec for this service were not available in the repo or the
  spec export; structure follows only the textual responsibilities in AC-1.
- Account-management APIs, OAuth 2.0/JWT flows and data schema are not specified.
- HTTPS termination, OpenAPI/Swagger, OpenTelemetry, Redis and OAuth2/OIDC client libraries
  are listed in the stack but not required by tasks T001–T004, so they are not wired in yet.

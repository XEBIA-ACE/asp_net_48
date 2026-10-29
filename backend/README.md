# Store Management Service (Java / Spring Boot) — SCAFFOLD-SVC-001

Backend skeleton living alongside the legacy .NET code in the repo root. Layers:
`presentation → application → domain → infrastructure` under `com.store`.
Customer/Product/Order controllers, services and repositories are **not** implemented here.

## Configuration (environment variables only)

| Variable | Required | Purpose |
|---|---|---|
| `DB_URL` | yes | JDBC URL, e.g. `jdbc:postgresql://host:5432/store` |
| `DB_USERNAME` | yes | DB user |
| `DB_PASSWORD` | yes | DB password |
| `DB_MAX_POOL_SIZE` | no (10) | Hikari pool size |
| `AUTH_JWK_SET_URI` | yes | JWKS URI of the OAuth2/OIDC provider |
| `SERVER_PORT` | no (8080) | HTTP port |

## Run / verify

    mvn verify          # tests + spotless check (`mvn spotless:apply` to format)
    mvn spring-boot:run
    curl localhost:8080/api/health   # {"status":"UP"}

## Open gaps / notes for reviewers

1. **AC-1 stack mismatch**: AC-1 asks to scaffold an Account Management Service on
   Node.js/OAuth2/Express, which conflicts with this Java/Spring Boot repo and plan. Not implemented;
   needs a decision (separate repo/story?). AC-1 is not addressed by this work.
2. **Health contract unspecified**: `HealthResponse`/`HealthStatusResponse` schemas were not in the
   provided export. `{"status":"UP"}` at `GET /api/health` is an assumption (path mirrors the
   existing .NET `HealthController`) and must be confirmed against SVC-001.
3. **ErrorResponse / GlobalExceptionHandler, DtoMapper, ValidationService** and the Customer/
   Product/Order contracts are not specified in the export, so no placeholders were invented.
4. **Existing .NET code** in the repo root is untouched; the plan does not say whether it is to be
   replaced or retained.
5. **Auth**: all endpoints other than `/api/health` require a valid JWT; issuer/audience validation
   and role rules are unspecified.
6. **Local verification not performed**: `java`/`mvn` are not installed in this environment, so the
   build, startup and health check were not run. A `@SpringBootTest` covers the health endpoint
   for when the toolchain is available.
7. No Dockerfile added (container platform listed in the stack, but not in the scaffold list).

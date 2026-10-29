# Spec gaps for US-001 (SVC-002)

Spec run f32a63e9-7181-47b9-8fb4-5fc1b80f8088. Each item below is a placeholder awaiting upstream clarification; none is a silent decision.

| ID | Spec reference | Gap | Placeholder used |
|----|----------------|-----|------------------|
| GAP-01 | FR-US-001-AC-1 / US-001-ac-1 | `EmailRegistrationRequest` and the user account have no field-level schema. No password or other credential is specified (the security obligation says only "validate credential and personal-data handling"). | Request has only `Email`; `User` has `Id`, `Email`, `CreatedAt`. No password is accepted or stored. |
| GAP-02 | FR-US-001-AC-2 / US-001-ac-2; design step "standardized Not Found" | `ErrorResponse` structure is not defined. | `{ Code, Message }`, also used for customer Not Found. |
| GAP-03 | FR-US-001-AC-1/2 | No email validation or normalization rules. | Trim; reject empty/unparseable (`System.Net.Mail.MailAddress`) with 400 `ErrorResponse`; uniqueness is case-insensitive. |
| GAP-04 | FR-US-001-AC-1 | Success status code and body are not specified. | `201 Created` with the created user (no `Location` header). |
| GAP-05 | US-001 title/behavior vs. acceptance criteria | The story is about customer CRUD, but both acceptance criteria are about `/api/v1/auth/register/email`. The functional spec says customers have only `name` and `city`; the technical design adds `id`, `createdAt`, `updatedAt`. | Both areas implemented. Customer follows the design (`Id`, `Name`, `City`, `CreatedAt`, `UpdatedAt`). |
| GAP-06 | Technical design (`SVC-002`) | Design names Java 17 / Spring Boot / PostgreSQL; the repo is ASP.NET Web API on .NET Framework 4.8 with an in-memory store (README mentions SQL Server). `service:SVC-002` is an unresolved reference. | Implemented in the existing ASP.NET 4.8 stack with `InMemoryDataStore`; no database added. Users are lost on restart. |
| GAP-07 | API impact: `/api/v1/customers` | Existing routes were `api/customers`. The spec lists only `/api/v1/customers`. | The prefix was changed to `api/v1/customers`; the old path no longer exists. Confirm no clients depend on it. |
| GAP-08 | Customer endpoints | No validation rules for customer `name`/`city` (design says mandatory). PATCH semantics unspecified. | No new validation added. PATCH applies only non-null `Name`/`City`. |
| GAP-09 | Known gaps in bundle | C3, C4 and API spec artifacts not generated. | None needed. |

## Verification note
The tests in `tests/MicroProjectApplication.Tests` trace to US-001-ac-1 and US-001-ac-2 (obligation IDs in `RegisterEmailAcceptanceTests`). They were written in an environment without a .NET/MSBuild toolchain and have **not been compiled or run**. A Windows CI agent with MSBuild and NuGet restore is required.

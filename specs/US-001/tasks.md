# Tasks: US-001

Implementation and verification tasks are ordered and independently reviewable. Each task traces back to specific functional requirements or acceptance criteria.

- [ ] T001 FR-US-001-AC-1: The system SHALL expose a POST endpoint at /api/v1/auth/register/email that accepts a request payload conforming to EmailRegistrationRequest and, when the provided email address is not already associated with an existing user in the database (i.e., the email is unique), SHALL create a new user account. (Trace: US-001-ac-1)
- [ ] T002 FR-US-001-AC-2: The system SHALL ensure that when a POST request is sent to /api/v1/auth/register/email with an EmailRegistrationRequest whose email already exists in the database, it responds with HTTP status code 409 (Conflict) and a response body conforming to ErrorResponse. (Trace: US-001-ac-2)
- [ ] T003 Design SVC-002 customer model and persistence layer changes based on the following design steps (Trace: service:SVC-002):
      - Within SVC-002, add a Customer entity constrained to fields id, name, city, createdAt, updatedAt, reusing the existing PostgreSQL dependency for persistence.
- [ ] T004 Implement /api/v1/customers CRUD REST endpoints in SVC-002 backed by the Customer entity and repositories (Trace: service:SVC-002):
      - POST /api/v1/customers
      - GET /api/v1/customers
      - GET /api/v1/customers/{id}
      - PUT /api/v1/customers/{id}
      - PATCH /api/v1/customers/{id}
      - DELETE /api/v1/customers/{id}
- [ ] T005 Implement standardized Not Found handling for /api/v1/customers/{id} operations so that requests for non-existent customer IDs return a consistent error payload (Trace: service:SVC-002).
- [ ] T006 Ensure product-related endpoints and models in SVC-002 are not modified or extended for this story, keeping the scope limited to customers (Trace: service:SVC-002).
- [ ] T007 Verify US-001-ac-1: Implement the /api/v1/auth/register/email POST endpoint to accept a valid EmailRegistrationRequest and create a new user account when the email is unique.
- [ ] T008 Verify US-001-ac-2: Ensure the /api/v1/auth/register/email POST endpoint returns a 409 error with an ErrorResponse when an email already exists in the database.

No additional tasks are specified in the source artifacts beyond those listed above.

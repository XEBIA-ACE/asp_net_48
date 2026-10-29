# User flows

## Dana automates product catalog updates
Dana discovers product endpoints, checks service health, and uses the API Explorer to create and update products reliably.

### Diagram

```mermaid
graph TD
classDef step fill:#e8f1ff,stroke:#333,stroke-width:1px,color:#000;

A[Home / Overview]
E[Products API]
G[Health Endpoint]
H[API Explorer / Try-It UI]

class A,E,G,H step;

A --> E
E --> G
G --> H
```

## Luis integrates order placement
Luis navigates entity docs to design order placement, ensuring valid references and consistent error handling across customers, products, and orders.

### Diagram

```mermaid
graph TD
classDef step fill:#e8f1ff,stroke:#333,stroke-width:1px,color:#000;

A[Home / Overview]
D[Customers API]
E[Products API]
F[Orders API]
C[Global Error & Validation Model]

class A,D,E,F,C step;

A --> D
D --> E
E --> F
F --> C
```

## Mei configures health-based monitoring
Mei uses the overview to find /health documentation, then reviews health semantics and error models to configure reliable monitoring.

### Diagram

```mermaid
graph TD
classDef step fill:#e8f1ff,stroke:#333,stroke-width:1px,color:#000;

A[Home / Overview]
G[Health Endpoint]
C[Global Error & Validation Model]

class A,G,C step;

A --> G
G --> C
```

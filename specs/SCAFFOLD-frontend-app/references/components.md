# Reusable UI components

## Primary Button
Clickable element for key actions such as 'Execute' in the Interactive Request Execution Panel and 'Try It' in the API Explorer.

- Usage: Use for primary actions where a clear call-to-action is needed, limited to one primary button per distinct context (e.g., one main execute per panel).
- States: default, hover, active/pressed, focus-visible, disabled, error-context
- Cross-platform notes: On web, implement hover and focus-visible separately with clear focus ring. On iOS and Android, emphasize pressed state via opacity reduction and/or elevation change; focus-visible applies for hardware keyboard scenarios. Maintain minimum touch target of 44x44dp/pt with 8px spacing around grouped buttons.

## Secondary Button
Lower-emphasis button for secondary actions such as 'Clear', 'Reset', or navigation between endpoint examples.

- Usage: Use when an action is optional or less prominent than the primary action within the same view.
- States: default, hover, active/pressed, focus-visible, disabled
- Cross-platform notes: On mobile, consider converting very low-value secondary actions to inline text links to reduce clutter. Respect OS-native button spacing and avoid stacking more than two buttons vertically without clear separation.

## Text Link
Inline textual navigation and action links within documentation content (e.g., cross-links to shared error model, auth overview, or related entity docs).

- Usage: Use sparingly in paragraphs to link to related sections like Global Error & Validation Model or other endpoint pages.
- States: default, hover, active/pressed, focus-visible, visited
- Cross-platform notes: Underlines should appear at least on hover and focus, and may be always-on for long-form docs to emphasize clickability. On mobile apps, use platform-native link patterns and ensure large enough tap area when used as a button alternative.

## Input Field
Text input for headers, path parameters, query parameters, and JSON payload editors (where not using a dedicated code editor).

- Usage: Used heavily in the Interactive Request Execution Panel and API Explorer, as well as any filter/search inputs in docs.
- States: default, hover, focus, disabled, error
- Cross-platform notes: Always pair with a visible label above the field and optional helper text below. Error state must include text explaining what went wrong (e.g., malformed payload, missing required field) and should align with the Global Error & Validation Model terminology.

## Dropdown / Select
Control for selecting HTTP methods, choosing endpoints within a resource, or picking example scenarios (e.g., success vs error examples).

- Usage: Use in the Interactive Request Execution Panel for HTTP method selection and any structured parameter presets.
- States: default, hover, focus, disabled
- Cross-platform notes: On mobile, prefer native pickers where possible. Ensure that options such as HTTP methods are ordered consistently (GET, POST, PUT/PATCH, DELETE) to support technical expectations.

## Card / Panel
Container for grouping related content such as endpoint summaries, request/response sections, or resource overviews on landing pages.

- Usage: Use for visually separating key sections: e.g., each core resource (customers, products, orders, /health) on the API overview, or each endpoint area in the API Explorer.
- States: default, hover, selected
- Cross-platform notes: On mobile, reduce padding and stacking multiple cards vertically; avoid deep nesting. Cards should be full-width with generous spacing between them for scannability.

## Tab / Segmented Control
Switches between related views such as Request/Response, Success/Error examples, or different entities in a compact space.

- Usage: Use within endpoint doc pages and the Interactive Request Execution Panel to switch between multiple sample languages or scenarios if needed.
- States: default, hover, active, focus-visible
- Cross-platform notes: On native platforms, map to standard segmented controls with clear selection state and label truncation for smaller screens.

## Badge / Status Pill
Compact indicator for HTTP methods (GET, POST, etc.) and status codes (2xx, 4xx, 5xx) in endpoint lists and examples.

- Usage: Label endpoints in lists and highlight response outcomes near examples, particularly on the Global Error & Validation Model and entity endpoints.
- States: GET, POST, PUT/PATCH, DELETE, 2xx, 4xx, 5xx
- Cross-platform notes: Badges should never be the sole carrier of meaning; always accompany with text labels. On mobile, ensure legible font sizes and sufficient padding to make them tappable where interactive.

## Table
Tabular layout for listing parameters, fields, error codes, roles, and permissions matrices.

- Usage: Use in Global Error & Validation Model, auth role matrices, and request parameter documentation where structured comparison is helpful.
- States: default, header, row-hover (web only)
- Cross-platform notes: On small screens, convert tables to stacked lists or cards to maintain readability. Ensure table headers are programmatically associated with cells for accessibility.

## Code Block / JSON Viewer
Read-only area for showing request/response examples, often in monospaced font with syntax highlighting.

- Usage: Used across endpoint docs and in the API Explorer’s response display (status, headers, body).
- States: default, copy-hover
- Cross-platform notes: Provide horizontal scrolling without wrapping JSON to preserve structure. Include a visible 'Copy' affordance with accessible labeling and focus handling.

## Toast / Inline Alert
Short-lived or inline messages used to communicate success, error, or informational feedback after actions in the API Explorer or minimal UI.

- Usage: Use success alerts after valid request execution (e.g., 2xx responses), error alerts when requests fail (4xx/5xx), and info alerts for [TBD] semantics or warnings.
- States: success, error, warning, info
- Cross-platform notes: On web and native, alerts must be announced by screen readers (e.g., ARIA live regions) and include clear text, not just color or icon, to indicate severity and meaning.

## Search Field [TBD]
Global search input for quickly locating endpoints or topics in the documentation (implementation details [TBD]).

- Usage: Placed in Global Navigation & Layout Shell when search is implemented; allows filtering by endpoint name, resource, or keyword.
- States: default, focus
- Cross-platform notes: Search semantics (scope, ranking) are [TBD]. Ensure results are keyboard accessible and screen-reader friendly once defined.

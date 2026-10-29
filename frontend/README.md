# API Documentation Frontend (SCAFFOLD-frontend-app)

Vite + React + TypeScript scaffold. Lives in `frontend/`, separate from the ASP.NET backend.

## Run locally

```
cd frontend
npm install
cp .env.example .env.local   # then edit
npm start
```

## Verify AC-1

`npm test` runs `src/test/ac1.smoke.test.tsx`: boots the app from env-only config, asserts SCR-001 and the
primary navigation render, that an empty environment still renders (no hardcoded endpoint), and that every
SCR-002..SCR-025 route renders its title. Manual check: `npm start`, open the printed URL, confirm the landing page and nav.

Other scripts: `npm run lint`, `npm run format`, `npm run build`.

## Environment variables

| Variable | Purpose |
| --- | --- |
| `VITE_API_BASE_URL` | Backend base URL. No default; if empty the landing page shows a notice. |
| `VITE_APP_TITLE` | Header/landing title (default "API Documentation"). |
| `VITE_FEATURE_API_EXPLORER` | `true` enables the `/explorer` routes. |
| `VITE_FEATURE_SEARCH` | `true` shows the Search Field placeholder ([TBD] semantics). |

## Notes and gaps

- No `specs/SCAFFOLD-frontend-app` or `references/components.md` exist in this repo; the design system values
  come from the task bundle (`src/design/tokens.ts`). Component set is in `src/components/ui.tsx`.
- Fonts are font-stack only (Inter / JetBrains Mono); no web-font files or external URLs are loaded.
- [TBD] and left as placeholders: Registration (SCR-002), detailed auth model (SCR-003), Delete endpoint docs
  (customers/products/orders), Interactive Request Execution Panel (SCR-024), Search Field semantics.
- SCR-011..SCR-021 titles follow the customer pattern and the existing controllers' actions
  (orders have no update); confirm against the UX artifacts.
- Screen content is stubbed (headings and placeholders only), pending detailed UX.

// AC-1: all runtime configuration is read from environment variables; nothing is hardcoded.
export interface AppConfig {
  /** Backend base URL. Empty string = not configured (UI shows a notice). */
  apiBaseUrl: string;
  appTitle: string;
  features: { apiExplorer: boolean; search: boolean };
}

type RawEnv = Record<string, unknown>;

const flag = (v: unknown) => String(v ?? '').toLowerCase() === 'true';

export function loadConfig(env: RawEnv = import.meta.env): AppConfig {
  return {
    apiBaseUrl: String(env.VITE_API_BASE_URL ?? '').replace(/\/+$/, ''),
    appTitle: String(env.VITE_APP_TITLE ?? '') || 'API Documentation',
    features: {
      apiExplorer: flag(env.VITE_FEATURE_API_EXPLORER),
      // Search Field semantics are [TBD]; the flag only gates showing the placeholder.
      search: flag(env.VITE_FEATURE_SEARCH),
    },
  };
}

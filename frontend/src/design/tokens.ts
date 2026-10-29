// Single source of truth for design tokens. Values come from the approved design system; do not add new ones.
export const colors = {
  primary: '#1F4F7F',
  secondary: '#4B5A6A',
  accent: '#2F855A',
  background: '#F5F7FA',
  text: '#1A202C',
  error: '#C53030',
  warning: '#D69E2E',
  info: '#3182CE',
  border: '#CBD5E0',
  muted: '#4A5568',
} as const;

export const primaryScale = {
  50: '#f2f6fa',
  100: '#dde7f2',
  200: '#bccfe5',
  300: '#8faad0',
  400: '#5f80b3',
  500: '#1F4F7F',
  600: '#183B5A',
  700: '#142f48',
  800: '#0f2336',
  900: '#0a1724',
} as const;

export const fonts = {
  ui: "'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif",
  mono: "'JetBrains Mono', 'Fira Code', 'SFMono-Regular', Menlo, Monaco, Consolas, 'Liberation Mono', 'Courier New', monospace",
} as const;

/** Flattened CSS custom properties, e.g. --color-primary, --color-primary-500, --font-ui. */
export function cssVariables(): Record<string, string> {
  const vars: Record<string, string> = {};
  for (const [k, v] of Object.entries(colors)) vars[`--color-${k}`] = v;
  for (const [k, v] of Object.entries(primaryScale)) vars[`--color-primary-${k}`] = v;
  vars['--font-ui'] = fonts.ui;
  vars['--font-mono'] = fonts.mono;
  return vars;
}

export function applyTokens(el: HTMLElement = document.documentElement) {
  for (const [k, v] of Object.entries(cssVariables())) el.style.setProperty(k, v);
}

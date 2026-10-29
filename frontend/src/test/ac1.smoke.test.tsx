// AC-1: app boots using only env-based configuration and renders the initial shell.
import { render, screen, within } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import App from '../App';
import { ConfigContext } from '../config/ConfigContext';
import { loadConfig } from '../config/env';
import { applyTokens } from '../design/tokens';
import { primaryNav, screens } from '../screens/registry';

const renderAt = (path: string, env: Record<string, string>) =>
  render(
    <ConfigContext.Provider value={loadConfig(env)}>
      <MemoryRouter initialEntries={[path]}>
        <App />
      </MemoryRouter>
    </ConfigContext.Provider>,
  );

describe('AC-1 scaffold smoke test', () => {
  it('renders SCR-001 and primary navigation from env config only', () => {
    renderAt('/', { VITE_API_BASE_URL: 'http://env.example/', VITE_APP_TITLE: 'Docs' });
    expect(screen.getByRole('heading', { level: 1, name: 'Docs' })).toBeInTheDocument();
    expect(screen.getByText('http://env.example')).toBeInTheDocument();
    const nav = within(screen.getByRole('navigation', { name: 'Primary' }));
    expect(nav.getAllByRole('link').map((a) => a.textContent)).toEqual(primaryNav.map((n) => n.label));
  });

  it('renders with an empty environment (no hardcoded endpoint)', () => {
    renderAt('/', {});
    expect(screen.getByText(/VITE_API_BASE_URL is not set/)).toBeInTheDocument();
  });

  it('every defined screen route renders its title', () => {
    for (const s of screens.filter((s) => s.path !== '/')) {
      const { unmount } = renderAt(s.path, { VITE_FEATURE_API_EXPLORER: 'true' });
      expect(screen.getByRole('heading', { level: 1, name: s.title })).toBeInTheDocument();
      unmount();
    }
  });

  it('exposes design tokens as CSS variables', () => {
    applyTokens();
    expect(document.documentElement.style.getPropertyValue('--color-primary')).toBe('#1F4F7F');
  });
});

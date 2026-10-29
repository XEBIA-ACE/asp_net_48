// SCR-025 layout shell: header + primary navigation + main outlet.
import { NavLink, Outlet } from 'react-router-dom';
import { useConfig } from './config/ConfigContext';
import { primaryNav } from './screens/registry';
import { SearchField } from './components/ui';

export default function Shell() {
  const { appTitle, features } = useConfig();
  return (
    <div className="shell">
      <header className="shell-header">
        <strong>{appTitle}</strong>
        {features.search && <SearchField />}
      </header>
      <nav className="shell-nav" aria-label="Primary">
        <ul>
          {primaryNav.map((n) => (
            <li key={n.to}>
              <NavLink to={n.to} end={n.to === '/'}>{n.label}</NavLink>
            </li>
          ))}
        </ul>
      </nav>
      <main className="shell-main"><Outlet /></main>
    </div>
  );
}

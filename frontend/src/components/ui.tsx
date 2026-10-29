// Minimal reusable components per references/components.md (design system). AC-1.
// Search Field is [TBD]: placeholder only, no search semantics.
import { useId, useState, type ButtonHTMLAttributes, type ReactNode } from 'react';
import { Link } from 'react-router-dom';

export const PrimaryButton = (p: ButtonHTMLAttributes<HTMLButtonElement>) => (
  <button type="button" {...p} className={`btn btn-primary ${p.className ?? ''}`} />
);
export const SecondaryButton = (p: ButtonHTMLAttributes<HTMLButtonElement>) => (
  <button type="button" {...p} className={`btn btn-secondary ${p.className ?? ''}`} />
);

export const TextLink = ({ to, children }: { to: string; children: ReactNode }) => (
  <Link to={to}>{children}</Link>
);

export function InputField({ label, ...rest }: { label: string } & React.InputHTMLAttributes<HTMLInputElement>) {
  const id = useId();
  return (
    <div>
      <label className="field-label" htmlFor={id}>{label}</label>
      <input id={id} className="input" {...rest} />
    </div>
  );
}

export function Dropdown({ label, options, ...rest }: { label: string; options: string[] } & React.SelectHTMLAttributes<HTMLSelectElement>) {
  const id = useId();
  return (
    <div>
      <label className="field-label" htmlFor={id}>{label}</label>
      <select id={id} className="select" {...rest}>
        {options.map((o) => <option key={o}>{o}</option>)}
      </select>
    </div>
  );
}

export const Card = ({ title, children }: { title?: string; children: ReactNode }) => (
  <section className="card">
    {title && <h2 style={{ marginTop: 0 }}>{title}</h2>}
    {children}
  </section>
);

/** Segmented control with arrow-key navigation (roving tabindex). */
export function Tabs({ tabs }: { tabs: { label: string; content: ReactNode }[] }) {
  const [active, setActive] = useState(0);
  const onKey = (e: React.KeyboardEvent) => {
    if (e.key === 'ArrowRight') setActive((active + 1) % tabs.length);
    if (e.key === 'ArrowLeft') setActive((active - 1 + tabs.length) % tabs.length);
  };
  return (
    <div>
      <div className="tabs" role="tablist" onKeyDown={onKey}>
        {tabs.map((t, i) => (
          <button key={t.label} role="tab" className="tab" aria-selected={i === active}
            tabIndex={i === active ? 0 : -1} onClick={() => setActive(i)}>{t.label}</button>
        ))}
      </div>
      <div role="tabpanel">{tabs[active].content}</div>
    </div>
  );
}

export type Tone = 'neutral' | 'success' | 'error' | 'warning' | 'info';
export const Badge = ({ tone = 'neutral', children }: { tone?: Tone; children: ReactNode }) => (
  <span className={`badge badge-${tone}`}>{children}</span>
);

export function Table({ columns, rows }: { columns: string[]; rows: ReactNode[][] }) {
  return (
    <table className="table">
      <thead><tr>{columns.map((c) => <th key={c} scope="col">{c}</th>)}</tr></thead>
      <tbody>{rows.map((r, i) => <tr key={i}>{r.map((c, j) => <td key={j}>{c}</td>)}</tr>)}</tbody>
    </table>
  );
}

export const CodeBlock = ({ children }: { children: string }) => (
  <pre className="code-block" tabIndex={0}><code>{children}</code></pre>
);

/** Inline alert (also usable as a toast body). */
export const Alert = ({ tone = 'info', children }: { tone?: Exclude<Tone, 'neutral'>; children: ReactNode }) => (
  <div className={`alert alert-${tone}`} role={tone === 'error' ? 'alert' : 'status'}>{children}</div>
);

/** [TBD] Search Field — semantics undecided; rendered disabled. */
export const SearchField = () => (
  <InputField label="Search [TBD]" type="search" disabled placeholder="[TBD – waiting on product decision]" />
);

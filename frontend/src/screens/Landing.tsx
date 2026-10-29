import { Alert, Card, CodeBlock, TextLink } from '../components/ui';
import { useConfig } from '../config/ConfigContext';
import { primaryNav } from './registry';

/** SCR-001 API Documentation Landing / Overview (skeleton). */
export default function Landing() {
  const { apiBaseUrl, appTitle } = useConfig();
  return (
    <>
      <h1>{appTitle}</h1>
      {!apiBaseUrl && (
        <Alert tone="warning">
          VITE_API_BASE_URL is not set. Documentation renders, but the API Explorer has no backend target.
        </Alert>
      )}
      <Card title="Backend base URL (from environment)">
        <CodeBlock>{apiBaseUrl || '(not configured)'}</CodeBlock>
      </Card>
      <Card title="Sections">
        <ul>
          {primaryNav.slice(1).map((n) => (
            <li key={n.to}><TextLink to={n.to}>{n.label}</TextLink></li>
          ))}
          <li><TextLink to="/registration">Registration Access Information</TextLink></li>
        </ul>
      </Card>
    </>
  );
}

import { Alert, Badge, Card } from '../components/ui';
import type { Screen } from './registry';

/** Skeleton screen: correct title/heading, content pending detailed UX. */
export default function StubScreen({ screen }: { screen: Screen }) {
  return (
    <>
      <h1>{screen.title}</h1>
      <p><Badge tone={screen.tbd ? 'warning' : 'neutral'}>{screen.id}</Badge></p>
      <Card>
        <p className="tbd">
          {screen.tbd
            ? '[TBD – waiting on product decision] Behavior for this screen is not yet specified.'
            : 'Content for this screen is not yet implemented (scaffold stub).'}
        </p>
      </Card>
      {screen.tbd && <Alert tone="warning">Placeholder only — no behavior has been assumed.</Alert>}
    </>
  );
}

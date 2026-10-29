import { Route, Routes } from 'react-router-dom';
import Shell from './Shell';
import Landing from './screens/Landing';
import StubScreen from './screens/StubScreen';
import { screens } from './screens/registry';
import { useConfig } from './config/ConfigContext';

export default function App() {
  const { features } = useConfig();
  return (
    <Routes>
      <Route element={<Shell />}>
        <Route index element={<Landing />} />
        {screens
          .filter((s) => s.path !== '/')
          .filter((s) => features.apiExplorer || !s.path.startsWith('/explorer'))
          .map((s) => (
            <Route key={s.id} path={s.path} element={<StubScreen screen={s} />} />
          ))}
        <Route path="*" element={<h1>Not found</h1>} />
      </Route>
    </Routes>
  );
}

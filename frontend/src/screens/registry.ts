// Screen definitions SCR-001..SCR-025. Titles for orders/products endpoint docs mirror the customer pattern;
// anything marked tbd renders an explicit "[TBD]" placeholder. Gap: Registration (SCR-002) flow is [TBD].
export interface Screen {
  id: string;
  path: string;
  title: string;
  tbd?: boolean;
}

const crud = (start: number, base: string, name: string, withUpdate: boolean): Screen[] => {
  const n = (i: number) => `SCR-${String(start + i).padStart(3, '0')}`;
  const list: Screen[] = [
    { id: n(0), path: `/${base}`, title: `${name} API Overview` },
    { id: n(1), path: `/${base}/create`, title: `${name} Create Endpoint Documentation` },
    { id: n(2), path: `/${base}/retrieve`, title: `${name} Retrieve by ID Endpoint Documentation` },
    { id: n(3), path: `/${base}/list`, title: `${name} List Endpoint Documentation` },
  ];
  if (withUpdate) list.push({ id: n(4), path: `/${base}/update`, title: `${name} Update Endpoint Documentation` });
  const del = withUpdate ? 5 : 4;
  list.push({ id: n(del), path: `/${base}/delete`, title: `${name} Delete Endpoint Documentation [TBD]`, tbd: true });
  return list;
};

export const screens: Screen[] = [
  { id: 'SCR-001', path: '/', title: 'API Documentation Landing / Overview' },
  { id: 'SCR-002', path: '/registration', title: 'Registration Access Information', tbd: true },
  { id: 'SCR-003', path: '/auth', title: 'Authentication & Authorization Overview', tbd: true },
  { id: 'SCR-004', path: '/errors', title: 'Global Error & Validation Model' },
  ...crud(5, 'customers', 'Customer', true),
  ...crud(11, 'products', 'Product', true),
  ...crud(17, 'orders', 'Order', false),
  { id: 'SCR-022', path: '/health', title: 'Service Health Endpoint Documentation' },
  { id: 'SCR-023', path: '/explorer', title: 'API Explorer / Interactive Docs' },
  { id: 'SCR-024', path: '/explorer/execute', title: 'Interactive Request Execution Panel', tbd: true },
  { id: 'SCR-025', path: '/layout-shell', title: 'Global Navigation & Layout Shell' },
];

/** Primary navigation, in specified order. */
export const primaryNav = [
  { label: 'Overview', to: '/' },
  { label: 'Auth', to: '/auth' },
  { label: 'Customers', to: '/customers' },
  { label: 'Products', to: '/products' },
  { label: 'Orders', to: '/orders' },
  { label: 'Health', to: '/health' },
  { label: 'Errors', to: '/errors' },
  { label: 'API Explorer', to: '/explorer' },
  { label: 'Layout Shell Spec', to: '/layout-shell' },
] as const;

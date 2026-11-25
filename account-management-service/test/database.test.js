'use strict';

const test = require('node:test');
const assert = require('node:assert/strict');
const {
  buildDataSourceOptions,
  connectDatabase,
} = require('../src/adapters/outbound/persistence/database');
const { loadConfig } = require('../src/config');

const dbConfig = {
  host: 'db.example',
  port: 5432,
  database: 'accounts',
  username: 'user',
  password: 'secret',
  ssl: false,
};

test('AC-1 / T004: builds PostgreSQL options from config without schema sync or migrations', () => {
  const options = buildDataSourceOptions(dbConfig);
  assert.equal(options.type, 'postgres');
  assert.equal(options.host, 'db.example');
  assert.equal(options.port, 5432);
  assert.equal(options.database, 'accounts');
  assert.equal(options.username, 'user');
  assert.equal(options.password, 'secret');
  assert.equal(options.synchronize, false);
  assert.equal(options.migrationsRun, false);
  assert.deepEqual(options.entities, []);
});

const hasDbEnv = ['DB_HOST', 'DB_PORT', 'DB_NAME', 'DB_USER', 'DB_PASSWORD'].every(
  (name) => process.env[name],
);

test(
  'AC-1 / T004: connects to PostgreSQL using environment configuration',
  { skip: !hasDbEnv && 'DB_* environment variables not set' },
  async () => {
    const config = loadConfig({ PORT: '3000', ...process.env });
    const dataSource = await connectDatabase(config.db);
    try {
      assert.equal(dataSource.isInitialized, true);
    } finally {
      await dataSource.destroy();
    }
  },
);

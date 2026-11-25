'use strict';

const test = require('node:test');
const assert = require('node:assert/strict');
const { loadConfig } = require('../src/config');

const validEnv = {
  PORT: '3000',
  DB_HOST: 'db.example.internal',
  DB_PORT: '5432',
  DB_NAME: 'accounts',
  DB_USER: 'svc',
  DB_PASSWORD: 'test-token',
};

test('SCAFFOLD-SVC-001 T002: loads configuration from environment', () => {
  const config = loadConfig(validEnv);
  assert.equal(config.port, 3000);
  assert.deepEqual(config.database, {
    host: 'db.example.internal',
    port: 5432,
    name: 'accounts',
    user: 'svc',
    password: validEnv.DB_PASSWORD,
  });
});

test('SCAFFOLD-SVC-001 T002: fails when required variables are missing', () => {
  assert.throws(
    () => loadConfig({ PORT: '3000' }),
    /Missing required environment variables: DB_HOST, DB_PORT, DB_NAME, DB_USER, DB_PASSWORD/,
  );
});

test('SCAFFOLD-SVC-001 T002: rejects invalid ports', () => {
  assert.throws(() => loadConfig({ ...validEnv, PORT: 'abc' }), /PORT must be an integer/);
  assert.throws(() => loadConfig({ ...validEnv, DB_PORT: '70000' }), /DB_PORT must be an integer/);
});

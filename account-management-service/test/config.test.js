'use strict';

const test = require('node:test');
const assert = require('node:assert/strict');
const { loadConfig } = require('../src/config');

const validEnv = {
  PORT: '3000',
  DB_HOST: 'db.example',
  DB_PORT: '5432',
  DB_NAME: 'accounts',
  DB_USER: 'user',
  DB_PASSWORD: 'secret',
};

test('AC-1 / T002: loads configuration from environment variables', () => {
  const config = loadConfig(validEnv);
  assert.equal(config.port, 3000);
  assert.deepEqual(config.db, {
    host: 'db.example',
    port: 5432,
    database: 'accounts',
    username: 'user',
    password: 'secret',
    ssl: false,
  });
});

test('AC-1 / T002: fails when required environment variables are missing', () => {
  const env = { ...validEnv };
  delete env.DB_PASSWORD;
  delete env.PORT;
  assert.throws(() => loadConfig(env), /Missing required environment variables: PORT, DB_PASSWORD/);
});

test('AC-1 / T002: rejects invalid ports', () => {
  assert.throws(() => loadConfig({ ...validEnv, DB_PORT: 'abc' }), /DB_PORT must be an integer/);
});

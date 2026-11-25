'use strict';

// SCAFFOLD-SVC-001 / T005 / AC-1: service starts from environment-only configuration
// and the health check responds. Requires a reachable PostgreSQL configured via DB_* vars.
const test = require('node:test');
const assert = require('node:assert/strict');
const { start } = require('../src/main');

const hasDb = Boolean(process.env.DB_HOST);

test(
  'AC-1: starts with env config, connects to PostgreSQL, /health responds',
  { skip: !hasDb && 'DB_HOST not set' },
  async () => {
    const { server, database, stop } = await start({ ...process.env, PORT: '0' });
    try {
      assert.equal(await database.ping(), true);
      const res = await fetch(`http://127.0.0.1:${server.address().port}/health`);
      assert.equal(res.status, 200);
      assert.deepEqual(await res.json(), { status: 'ok' });
    } finally {
      await stop();
    }
  },
);

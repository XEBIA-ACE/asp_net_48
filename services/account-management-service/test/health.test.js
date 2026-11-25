'use strict';

const test = require('node:test');
const assert = require('node:assert/strict');
const request = require('supertest');
const { createApp } = require('../src/adapters/inbound/http/app');

test('SCAFFOLD-SVC-001 T003: GET /health returns 200 with status ok', async () => {
  const res = await request(createApp()).get('/health');
  assert.equal(res.status, 200);
  assert.deepEqual(res.body, { status: 'ok' });
});

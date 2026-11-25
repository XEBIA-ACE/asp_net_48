'use strict';

const test = require('node:test');
const assert = require('node:assert/strict');
const request = require('supertest');
const { createApp } = require('../src/adapters/inbound/http/app');

test('AC-1 / T003: GET /health responds 200 with status ok', async () => {
  const res = await request(createApp()).get('/health');
  assert.equal(res.status, 200);
  assert.deepEqual(res.body, { status: 'ok' });
});

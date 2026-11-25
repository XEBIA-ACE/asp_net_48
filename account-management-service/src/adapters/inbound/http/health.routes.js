'use strict';

// SCAFFOLD-SVC-001 / T003: minimal health-check endpoint.

const { Router } = require('express');

function healthRouter() {
  const router = Router();
  router.get('/health', (_req, res) => {
    res.status(200).json({ status: 'ok' });
  });
  return router;
}

module.exports = { healthRouter };

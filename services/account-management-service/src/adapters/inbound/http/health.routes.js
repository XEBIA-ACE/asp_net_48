'use strict';

const { Router } = require('express');

// SCAFFOLD-SVC-001 / T003: liveness check only; no business logic.
function healthRouter() {
  const router = Router();
  router.get('/health', (req, res) => {
    res.status(200).json({ status: 'ok' });
  });
  return router;
}

module.exports = { healthRouter };

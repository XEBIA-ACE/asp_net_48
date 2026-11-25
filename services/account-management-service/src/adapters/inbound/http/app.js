'use strict';

const express = require('express');
const { healthRouter } = require('./health.routes');

function createApp() {
  const app = express();
  app.disable('x-powered-by');
  app.use(express.json());
  app.use(healthRouter());
  return app;
}

module.exports = { createApp };

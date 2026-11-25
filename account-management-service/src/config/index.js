'use strict';

// SCAFFOLD-SVC-001 / T002: environment-based configuration loading.
// Values come only from process.env (optionally populated from an uncommitted .env file).

const REQUIRED_VARS = ['PORT', 'DB_HOST', 'DB_PORT', 'DB_NAME', 'DB_USER', 'DB_PASSWORD'];

function parsePort(name, value) {
  const port = Number(value);
  if (!Number.isInteger(port) || port < 1 || port > 65535) {
    throw new Error(`Invalid configuration: ${name} must be an integer between 1 and 65535`);
  }
  return port;
}

function loadConfig(env = process.env) {
  const missing = REQUIRED_VARS.filter((name) => env[name] === undefined || env[name] === '');
  if (missing.length > 0) {
    throw new Error(`Missing required environment variables: ${missing.join(', ')}`);
  }

  return Object.freeze({
    port: parsePort('PORT', env.PORT),
    db: Object.freeze({
      host: env.DB_HOST,
      port: parsePort('DB_PORT', env.DB_PORT),
      database: env.DB_NAME,
      username: env.DB_USER,
      password: env.DB_PASSWORD,
      ssl: env.DB_SSL === 'true',
    }),
  });
}

module.exports = { loadConfig, REQUIRED_VARS };

'use strict';

// SCAFFOLD-SVC-001 / T002: all ports, endpoints and credentials come from the environment.
const REQUIRED = ['PORT', 'DB_HOST', 'DB_PORT', 'DB_NAME', 'DB_USER', 'DB_PASSWORD'];

function parsePort(name, value) {
  const port = Number(value);
  if (!Number.isInteger(port) || port < 0 || port > 65535) {
    throw new Error(`Invalid configuration: ${name} must be an integer between 0 and 65535`);
  }
  return port;
}

function loadConfig(env = process.env) {
  const missing = REQUIRED.filter((name) => env[name] === undefined || env[name] === '');
  if (missing.length > 0) {
    throw new Error(`Missing required environment variables: ${missing.join(', ')}`);
  }

  return Object.freeze({
    port: parsePort('PORT', env.PORT),
    database: Object.freeze({
      host: env.DB_HOST,
      port: parsePort('DB_PORT', env.DB_PORT),
      name: env.DB_NAME,
      user: env.DB_USER,
      password: env.DB_PASSWORD,
    }),
  });
}

module.exports = { loadConfig, REQUIRED };

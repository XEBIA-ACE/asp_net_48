'use strict';

// SCAFFOLD-SVC-001 / T001: service entrypoint.

require('dotenv').config();

const { loadConfig } = require('./config');
const { createApp } = require('./adapters/inbound/http/app');
const { connectDatabase } = require('./adapters/outbound/persistence/database');

async function main() {
  const config = loadConfig();
  const dataSource = await connectDatabase(config.db);
  const server = createApp().listen(config.port, () => {
    console.log(`account-management-service listening on port ${config.port}`);
  });

  const shutdown = () => {
    server.close(() => {
      dataSource.destroy().finally(() => process.exit(0));
    });
  };
  process.on('SIGINT', shutdown);
  process.on('SIGTERM', shutdown);
}

main().catch((err) => {
  console.error(`account-management-service failed to start: ${err.message}`);
  process.exit(1);
});

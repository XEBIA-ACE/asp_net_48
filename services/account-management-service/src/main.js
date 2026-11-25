'use strict';

const { loadConfig } = require('./config');
const { createApp } = require('./adapters/inbound/http/app');
const { createDatabaseConnection } = require('./adapters/outbound/persistence/database');

async function start(env = process.env) {
  const config = loadConfig(env);
  const database = createDatabaseConnection(config.database);
  await database.connect();

  const app = createApp();
  const server = await new Promise((resolve, reject) => {
    const s = app.listen(config.port, () => resolve(s));
    s.on('error', reject);
  });

  const stop = async () => {
    await new Promise((resolve) => server.close(resolve));
    await database.close();
  };

  return { server, database, stop };
}

if (require.main === module) {
  start()
    .then(({ server, stop }) => {
      console.log(`account-management-service listening on port ${server.address().port}`);
      const shutdown = () => stop().then(() => process.exit(0));
      process.on('SIGINT', shutdown);
      process.on('SIGTERM', shutdown);
    })
    .catch((err) => {
      console.error(`account-management-service failed to start: ${err.message}`);
      process.exit(1);
    });
}

module.exports = { start };

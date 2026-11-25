'use strict';

const { DataSource } = require('typeorm');

// SCAFFOLD-SVC-001 / T004: PostgreSQL connection helper (TypeORM). No entities or migrations.
function createDatabaseConnection(databaseConfig) {
  const dataSource = new DataSource({
    type: 'postgres',
    host: databaseConfig.host,
    port: databaseConfig.port,
    database: databaseConfig.name,
    username: databaseConfig.user,
    password: databaseConfig.password,
    entities: [],
    migrations: [],
    synchronize: false,
    logging: false,
  });

  return {
    async connect() {
      if (!dataSource.isInitialized) {
        await dataSource.initialize();
      }
    },
    async ping() {
      await dataSource.query('SELECT 1');
      return true;
    },
    async close() {
      if (dataSource.isInitialized) {
        await dataSource.destroy();
      }
    },
  };
}

module.exports = { createDatabaseConnection };

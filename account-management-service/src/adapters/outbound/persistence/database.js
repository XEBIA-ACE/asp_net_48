'use strict';

// SCAFFOLD-SVC-001 / T004: PostgreSQL connection helper (TypeORM), configured via environment.
// No entities, schemas, or migrations are defined under this story.

const { DataSource } = require('typeorm');

function buildDataSourceOptions(dbConfig) {
  return {
    type: 'postgres',
    host: dbConfig.host,
    port: dbConfig.port,
    database: dbConfig.database,
    username: dbConfig.username,
    password: dbConfig.password,
    ssl: dbConfig.ssl ? { rejectUnauthorized: true } : false,
    entities: [],
    migrations: [],
    synchronize: false,
    migrationsRun: false,
    logging: false,
  };
}

function createDataSource(dbConfig) {
  return new DataSource(buildDataSourceOptions(dbConfig));
}

async function connectDatabase(dbConfig) {
  const dataSource = createDataSource(dbConfig);
  await dataSource.initialize();
  await dataSource.query('SELECT 1');
  return dataSource;
}

module.exports = { buildDataSourceOptions, createDataSource, connectDatabase };

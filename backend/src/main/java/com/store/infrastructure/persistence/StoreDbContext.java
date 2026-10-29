package com.store.infrastructure.persistence;

import com.store.config.AppConfig;
import com.zaxxer.hikari.HikariConfig;
import com.zaxxer.hikari.HikariDataSource;
import javax.sql.DataSource;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

/**
 * Database connection helper for PostgreSQL (or a compatible database). Configured strictly from
 * {@link AppConfig}; repositories obtain connections through the resulting {@link DataSource}.
 */
@Configuration
public class StoreDbContext {

  @Bean
  public DataSource dataSource(AppConfig config) {
    AppConfig.Database db = config.database();
    HikariConfig hikari = new HikariConfig();
    hikari.setJdbcUrl(db.url());
    hikari.setUsername(db.username());
    hikari.setPassword(db.password());
    hikari.setMaximumPoolSize(db.maxPoolSize());
    // Start without requiring the database to be reachable.
    hikari.setInitializationFailTimeout(-1);
    return new HikariDataSource(hikari);
  }
}

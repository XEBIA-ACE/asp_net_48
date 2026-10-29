package com.store.config;

import org.springframework.boot.context.properties.ConfigurationProperties;

/**
 * Typed service settings. Values are bound from environment variables via application.yml; no
 * secrets, URLs or credentials are defaulted in code.
 */
@ConfigurationProperties(prefix = "app")
public record AppConfig(Database database, Auth auth) {

  public record Database(String url, String username, String password, Integer maxPoolSize) {}

  /** JWKS endpoint of the OAuth2/OIDC provider used to validate bearer tokens. */
  public record Auth(String jwkSetUri) {}
}

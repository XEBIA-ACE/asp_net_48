package com.store.presentation;

import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.TestPropertySource;
import org.springframework.test.web.servlet.MockMvc;

@SpringBootTest
@AutoConfigureMockMvc
@TestPropertySource(
    properties = {
      "DB_URL=jdbc:postgresql://localhost:5432/test",
      "DB_USERNAME=test",
      "DB_PASSWORD=test",
      "AUTH_JWK_SET_URI=http://localhost:9999/jwks"
    })
class HealthControllerTest {

  @Autowired MockMvc mvc;

  @Test
  void healthIsPublicAndReturnsStatus() throws Exception {
    mvc.perform(get("/api/health"))
        .andExpect(status().isOk())
        .andExpect(jsonPath("$.status").value("UP"));
  }
}

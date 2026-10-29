package com.store.presentation;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/health")
public class HealthController {

  /** Liveness only; deliberately exposes no diagnostics. */
  public record HealthResponse(String status) {}

  @GetMapping
  public HealthResponse get() {
    return new HealthResponse("UP");
  }
}

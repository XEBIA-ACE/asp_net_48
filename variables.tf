variable "region" {
  type        = string
  description = "AWS region to deploy into."
  default     = "us-east-1"
}

variable "environment" {
  type        = string
  description = "Deployment environment (e.g., dev, prod)."
  default     = "dev"
}

variable "project" {
  type        = string
  description = "Project identifier used for tagging and naming."
  default     = "s3-bucket-project"
}

variable "data_bucket_prefix" {
  type        = string
  description = "Prefix for the data S3 bucket; a unique suffix will be added."
  default     = "data-bucket"
  validation {
    condition = can(regex("^[a-z0-9-]{3,37}$", var.data_bucket_prefix))
    error_message = "data_bucket_prefix must be 3-37 chars, lowercase letters, numbers, and hyphens only."
  }
}

variable "log_bucket_prefix" {
  type        = string
  description = "Prefix for the access log S3 bucket; a unique suffix will be added."
  default     = "access-logs"
  validation {
    condition = can(regex("^[a-z0-9-]{3,37}$", var.log_bucket_prefix))
    error_message = "log_bucket_prefix must be 3-37 chars, lowercase letters, numbers, and hyphens only."
  }
}

variable "log_expiration_days" {
  type        = number
  description = "Number of days before access log objects are expired."
  default     = 90
  validation {
    condition     = var.log_expiration_days >= 30
    error_message = "log_expiration_days must be at least 30 days."
  }
}

variable "data_ia_transition_days" {
  type        = number
  description = "Days before transitioning data objects to STANDARD_IA."
  default     = 30
  validation {
    condition     = var.data_ia_transition_days >= 30
    error_message = "data_ia_transition_days must be at least 30 days."
  }
}
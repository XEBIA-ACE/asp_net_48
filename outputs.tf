output "data_bucket_name" {
  description = "Name of the primary data S3 bucket."
  value       = aws_s3_bucket.data.bucket
}

output "data_bucket_arn" {
  description = "ARN of the primary data S3 bucket."
  value       = aws_s3_bucket.data.arn
}

output "log_bucket_name" {
  description = "Name of the S3 access log bucket."
  value       = aws_s3_bucket.logs.bucket
}

output "log_bucket_arn" {
  description = "ARN of the S3 access log bucket."
  value       = aws_s3_bucket.logs.arn
}

output "data_bucket_kms_key_arn" {
  description = "ARN of the KMS key used for data bucket encryption."
  value       = aws_kms_key.data_bucket.arn
}
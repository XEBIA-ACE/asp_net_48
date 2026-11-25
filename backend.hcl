# Example backend configuration, to be used with:
# terraform init -backend-config=backend.hcl
bucket = "CHANGE-ME-TERRAFORM-STATE-BUCKET"
key    = "s3-dev/terraform.tfstate"
region = "us-east-1"
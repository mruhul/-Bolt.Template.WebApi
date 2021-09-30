terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 3.0"
    }
  }
  backend "s3" {
    bucket = "__tf_bucket__"
    key    = "__group_name__-__app_name__-stack"
    region = "ap-southeast-2"
  }
}
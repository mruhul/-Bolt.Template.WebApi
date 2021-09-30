variable "app_name" {
  type    = string
  default = "__app_name__"
}

variable "app_port" {
  default = 80
}

variable "app_health_check_path" {
  default = "/ping"
}

variable "app_count" {
  description = "Number of docker containers to run"
  default     = __app_count__
}

variable "app_image" {
  default = "nginx"
}

variable "fargate_cpu" {
  description = "Fargate instance CPU units to provision (1 vCPU = 1024 CPU units)"
  default     = "__app_cpu__"
}

variable "fargate_memory" {
  description = "Fargate instance memory to provision (in MiB)"
  default     = "__app_memory__"
}


variable "group" {
  type    = string
  default = "__group_name__"
}

variable "env" {
  type    = string
  default = "__default_env__"
}

variable "aws_region" {
  description = "The AWS region to create things in."
  default     = "__aws_region__"
}

variable "ecs_task_execution_role_name" {
  description = "ECS task execution role name"
  default     = "role-ecs-task-execution"
}

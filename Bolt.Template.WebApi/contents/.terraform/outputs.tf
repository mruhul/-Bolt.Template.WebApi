# outputs.tf

output "alb_hostname" {
  value = data.aws_alb.main.dns_name
}

output "ecs_task_def" {
  value = aws_ecs_task_definition.app.arn
}

resource "aws_cloudwatch_log_group" "app" {
  name              = "/ecs/${var.group}/${var.app_name}-${var.env}"
  retention_in_days = 30


  tags = {
    Group   = var.group
    Env     = var.env
    AppName = var.app_name
    Name    = "loggroup-${var.group}-${var.app_name}-${var.env}"
  }
}

resource "aws_cloudwatch_log_stream" "app" {
  name           = "log-stream-${var.group}-${var.app_name}-${var.env}"
  log_group_name = aws_cloudwatch_log_group.app.name
}
